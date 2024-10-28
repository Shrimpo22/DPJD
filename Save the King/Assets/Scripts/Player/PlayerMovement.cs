using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
    private float initialHeight;
    private Vector3 initialCenter;
    public bool drawGizmos = false;
    public float speed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    private Vector3 velocity;
    private CharacterController controller;

    public bool isTrapped = false;
    public bool isCrouching = false;


    PlayerControls controls;
    [SerializeField] Vector2 moveInput;
    [SerializeField] Transform camTransform;

    private Animator animator;
    public Collider swordColider;
    public PlayerAttack playerAttack;

    private Vector3 currentMovement;
    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    [SerializeField]
    private bool isGrounded;
    private float targetSpeed;
    private bool isSprinting;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
        isGrounded = controller.isGrounded;
        initialHeight = controller.height;
        initialCenter = controller.center;
    }

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => moveInput = Vector2.zero;
        controls.Gameplay.Crouch.performed += ctx => HandleCrouch();
        controls.Gameplay.Sprint.performed += ctx => { isSprinting = true; targetSpeed = 10f;};
        controls.Gameplay.Sprint.canceled += ctx => { isSprinting = false; targetSpeed = 6f;};

    }

    void OnEnable(){
        controls.Gameplay.Enable();
    }

    void OnDisable(){
        controls.Gameplay.Disable();
    }

    public void EnableCollider()
    {
        playerAttack.EnableSwordCollider();
    }

    public void DisableCollider()
    {
        playerAttack.DisabeSwordCollider();
    }

    void Update()
    {
        handleInteraction();
        if(!isTrapped)
            handleMovement();
    }

    private void HandleCrouch(){
        isCrouching = !isCrouching; 
        animator.SetBool("isCrouching", isCrouching);
        if(isCrouching){
           controller.height /=2; 
           controller.center /=2; 
        }else{
           controller.height = initialHeight; 
           controller.center = initialCenter; 
        }
    }

    private void handleInteraction()
    {
        if (isTrapped && controls.Gameplay.Interaction.triggered)
        {
            isTrapped = false;
            animator.SetBool("isHurt", false);
        }
        else if (isTrapped)
        {
            speed = 0f;
            animator.SetFloat("movementSpeed", speed);
            animator.SetBool("isHurt", true);
        }
    }

    void handleMovement(){

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if ((stateInfo.IsName("hit1") || stateInfo.IsName("hit2") || stateInfo.IsName("hit3")) && stateInfo.normalizedTime <= 1f){
            targetSpeed = 0f;    
        }else if (!isSprinting && !isCrouching)
            targetSpeed = 6f;
        else if (isCrouching)
            targetSpeed = 3f;

        if (!isGrounded && velocity.y < 0)
        {
            velocity.y = -9.81f;  
        }
        else velocity.y += gravity * Time.deltaTime;

        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        // Only move if there's input
        if (direction.magnitude > 0f && !stateInfo.IsName("hit1"))
        {
            // Get the angle from the camera's forward direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;

            // Smooth the turning of the character
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move direction relative to the camera
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move((moveDir.normalized * speed + velocity) * Time.deltaTime);
        }else{
            if(!stateInfo.IsName("hit1")){
                targetSpeed = 0f;
                controller.Move(velocity * Time.deltaTime);
            }
        }

        speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * 4f);
        animator.SetFloat("movementSpeed", speed);
        
        animator.SetBool("isMoving", direction.magnitude > 0f);

        if(controls.Gameplay.LightAttack.triggered && controller.isGrounded)
        {
            isCrouching = false;
            speed = 0;
            animator.SetTrigger("attack");
            animator.SetBool("isCrouching", isCrouching);            


            if (playerAttack != null)
            {
                playerAttack.PerformAttack(); 
            }
        }
    }

    void OnDrawGizmos(){
        if(drawGizmos){
        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;
        forward.y = 0;
        right.y = 0;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + forward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + right);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.right);

        Vector3 forwardRelative = moveInput.y * forward;
        Vector3 rightRelative = moveInput.x * right;

        Vector3 movement = forwardRelative + rightRelative;
        Gizmos.color = Color.gray;
        Gizmos.DrawLine(transform.position, transform.position + movement);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position,transform.position + currentMovement);
        }
    }
}

