using System;
using System.Collections;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    private float initialHeight;
    private Vector3 initialCenter;
    public bool drawGizmos = false;
    public float speed = 4.25f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    private Vector3 velocity;
    private CharacterController controller;

    public bool isTrapped = false;
    public bool isCrouching = false;
    public bool isDodging = false;
    public bool isSwordEquipped = false;


    // Interaction variables
    private int interactionCount = 0;
    private int interactionGoal = 20;
    public float barDecreaseSpeed = 10.0f;
    public Image interactionProgressBar;  // Reference to the circular progress bar UI
    public TextMeshProUGUI interactionText;  // Reference to the TextMeshPro for interaction info

    private float interactionProgress = 0f;

    [SerializeField] Avatar avatar;

    public string trappedLoopAnimation = "AttemptEscape";
    private float interactionCooldown = 2.0f;  // Timer for 5 seconds of inactivity
    private float inactivityTimer = 0f;  // Tracks the time player is inactive

    PlayerControls controls;
    [SerializeField] Vector2 moveInput;
    [SerializeField] Transform camTransform;

    public Animator animator;
    public Collider swordColider;
    public PlayerAttack playerAttack;

    private Vector3 currentMovement;
    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    [SerializeField]
    private bool isGrounded;
    private float targetSpeed;
    private bool isSprinting;
    private bool isDetected;
    
    public Transform handTransform;



    void Start()
    {
        // Initially hide the UI elements
        interactionProgressBar.gameObject.SetActive(false);
        interactionText.gameObject.SetActive(false);

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
        isGrounded = controller.isGrounded;
        initialHeight = controller.height;
        initialCenter = controller.center;
        
    }

    void Awake()
    {
        controls = InputManager.inputActions;
        controls.Gameplay.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => moveInput = Vector2.zero;
        controls.Gameplay.Crouch.performed += ctx => HandleCrouch();
        controls.Gameplay.Sprint.performed += ctx => { isSprinting = true; targetSpeed = 6f;};
        controls.Gameplay.Sprint.canceled += ctx => { isSprinting = false; targetSpeed = 4.25f;};
        controls.Gameplay.Dodging.performed += ctx => animator.SetTrigger("Dodge");
    }

    void OnEnable(){
        controls.Gameplay.Enable();
    }

    void OnDisable(){
        //controls.Gameplay.Disable();
    }

    public void EnableCollider()
    {
        playerAttack.EnableSwordCollider();
    }

    public void DisableCollider()
    {
        playerAttack.DisabeSwordCollider();
    }

    public void EnableInvincibility()
    {
        this.tag = "Untagged";
    }

    public void DisableInvincibility()
    {
        this.tag = "Player";
    }
    void Update()
    {
        handleInteraction();
        if(!isTrapped)
            handleMovement();
    }


    public void EnableMovement()
    {
        controls.Gameplay.Movement.Enable();
        controls.Gameplay.Sprint.Enable();
    }

    public void DisableMovement()
    {
        controls.Gameplay.Movement.Disable();
        controls.Gameplay.Sprint.Disable();
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
        if (isTrapped)
        {
            animator.SetBool("isHurt", true);

            // Show the UI elements (Progress bar and Text) when trapped
            interactionProgressBar.gameObject.SetActive(true);
            interactionText.gameObject.SetActive(true);

            speed = 0f;
            animator.SetFloat("movementSpeed", speed);

            if (controls.Gameplay.Interaction.triggered)
            {
                // Trigger the infinite animation when trapped
                animator.SetBool(trappedLoopAnimation, true);

                interactionCount++;  // Increase the interaction count

                // update the progress bar
                interactionProgress = (float)interactionCount / interactionGoal;
                interactionProgressBar.fillAmount = interactionProgress;

                inactivityTimer = 0f;  // Reset the inactivity timer since player interacted

                // Check if interaction count reached the goal
                if (interactionCount >= interactionGoal)
                {
                    isTrapped = false;
                    ResetInteraction();  // Reset interaction-related variables
                }
            }

            // Increment the inactivity timer when no interaction happens
            inactivityTimer += Time.deltaTime;

            // Check if the player has been inactive in the interaction
            if (inactivityTimer >= interactionCooldown)
            {

                DecreaseProgressBar();

                animator.SetBool("isHurt", true);  // Trigger "isHurt" animation
                animator.SetBool(trappedLoopAnimation, false);  // Stop the looping escape animation
                inactivityTimer = 0f;  // Reset the timer
            }

        }
    }

    // If the player stops interacting, decrease the progress bar slowly
    private void DecreaseProgressBar()
    {

        interactionProgress -= barDecreaseSpeed * Time.deltaTime; //alterar pois o Time.deltatime esta a fazer com que fique muito lento o decrease
        interactionProgressBar.fillAmount = interactionProgress;

        // Reset interaction count when progress bar reaches 0
        if (interactionProgress <= 0f)
        {
            interactionCount = 0;
            interactionProgress = 0f;
        }
    }

    private void ResetInteraction()
    {
        interactionCount = 0;
        interactionProgress = 0f;
        interactionProgressBar.fillAmount = 0f;  // Reset the progress bar

        animator.SetBool("isHurt", false);
        animator.SetBool(trappedLoopAnimation, false);

        // Hide the UI elements (Progress bar and Text) once the trap is released
        interactionProgressBar.gameObject.SetActive(false);
        interactionText.gameObject.SetActive(false);
    }

    void handleMovement(){

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if ((stateInfo.IsName("hit1") || stateInfo.IsName("hit2") || stateInfo.IsName("hit3") || stateInfo.IsName("heavy1") || stateInfo.IsName("heavy2") && stateInfo.normalizedTime <= 1f)){
            targetSpeed = 0f;    
        }else if (!isSprinting && !isCrouching)
            targetSpeed = 4.25f;
        else if (isCrouching)
            targetSpeed = 2f;

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

        if (controls.Gameplay.LightAttack.triggered && controller.isGrounded && isSwordEquipped && !isCrouching)
        {
            
            animator.SetTrigger("attack");
            animator.SetBool("isCrouching", isCrouching);


            if (playerAttack != null)
            {
                playerAttack.PerformAttack();
            }
        }

        if (controls.Gameplay.HeavyAttack.triggered && controller.isGrounded && isSwordEquipped && !isCrouching)
        {
            animator.SetTrigger("heavy");
            animator.SetBool("isCrouching", isCrouching);

            if (playerAttack != null)
            {
                playerAttack.PerformAttack();
            }
        }



        speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * 4f);
        animator.SetFloat("movementSpeed", speed);
        
        animator.SetBool("isMoving", direction.magnitude > 0f);
    }


    public void stealthAttack()
    {
        if (controls.Gameplay.LightAttack.triggered && isSwordEquipped && !isDetected)
        {
            animator.Play("stabbing");
        }
    }

    public void Detected()
    {
        Debug.Log("Nahh fui visto");
        isDetected = true;
    }

    public void NotDetected()
    {
        Debug.Log("Estou no escuro");
        isDetected = false;
    }

    public bool IsDetected() {
        return isDetected;
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