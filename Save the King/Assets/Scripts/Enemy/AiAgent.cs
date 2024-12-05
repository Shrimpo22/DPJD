using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public bool isNpc = false;
    public bool drawAlertState;
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Transform playerTransform;
    public AiStateId currentState;
    public AiSensor sensor;

    public Weapons weapon;
    public float alertState = 0;
    public float alertRate = 0;
    public float maxHealth;
    public float currentHealth;
    public Transform[] patrolPoints;  // Array of waypoints 
    public bool drawPatrol;
    public float targetSpeed;
    public float currentSpeed;


    CinemachineImpulseSource screenShake;
    float powerAmount;
    public Animator animator;
    Collider collid;

    public bool canChangeState = true;

    [SerializeField]UIHealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = config.maxHealth;
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<UIHealthBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        sensor = GetComponentInChildren<AiSensor>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent.speed = config.speed;
        navMeshAgent.stoppingDistance = 0;
        alertRate = config.alertRate;
        powerAmount = config.powerAmount;
        weapon = GetComponentInChildren<Weapons>();

        screenShake = GetComponent<CinemachineImpulseSource>();
        collid = GetComponent<Collider>();

        
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiLookForPlayerState());
        stateMachine.RegisterState(new AiAttackState());
        stateMachine.ChangeState(initialState);
        
    }

    void Update()
    {
        if(isNpc) return;
        stateMachine.Update();
        if(navMeshAgent.hasPath){
            targetSpeed = navMeshAgent.speed;
        }else{
            targetSpeed = 0;
        }
        
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 4f);

        animator.SetFloat("speed", currentSpeed);
    }
    
    public void IncreaseAlertState(){
        if(alertState < 1){
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
            float distanceFactor = Mathf.Log(1/distanceToPlayer + 1) * 8;
            if(playerTransform.GetComponent<PlayerMovement>().isCrouching)
                distanceFactor /= 2;
            alertState += alertRate * Time.deltaTime * distanceFactor;
        }else{
            if(canChangeState){
                ScreenShake((transform.position - sensor.Objects[0].transform.position).normalized);    
                stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }
    }

    public void SetAlertState(float amount){
        alertState = amount;
    }
    public void ScreenShake(Vector3 dir){
        Debug.Log("ScreenShake made?");
        screenShake.GenerateImpulseWithVelocity(dir);
    }
    public void DecreaseAlertState(){
        if(alertState > 0)
            alertState -= alertRate * Time.deltaTime;
        if(alertState < alertRate)
            alertState = 0;
    }
    public void TakeDamage(float amount){
        if(isNpc) return;
        currentHealth -= amount;
        healthBar.SetHealthBarPercentage(currentHealth/maxHealth);

        if(currentHealth > 0){
            animator.Play("EnemyGetHit",0,0f);
            SetAlertState(1.2f);
            stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
        if(currentHealth <= 0.0f){
            animator.Play("FallingDeath");
            healthBar.gameObject.SetActive(false);
            collid.enabled = false;
            sensor.enabled = false;
            stateMachine.ChangeState(AiStateId.Death);
        }
    }
    
    public void EnableCollider(){
        weapon.EnableCollider();
    }
    public void DisableCollider(){
        weapon.DisableCollider();
    }

    public void ClearHits(){
        weapon.RemoveHitTargets();
    }

    void OnDrawGizmos(){
        if(drawAlertState){
            Gizmos.color = Color.white;
            if(alertState > 0 && alertState <= 0.25)
                Gizmos.color = Color.yellow;
            if(alertState > 0.25 && alertState < 0.75)
                Gizmos.color = Color.magenta;
            if(alertState > 0.75 && alertState <= 1)
                Gizmos.color = Color.red;  
            Gizmos.DrawSphere(transform.position, 0.5f);      
        }
        if(drawPatrol && patrolPoints.Length > 0){
            
            Transform lastTransform = patrolPoints[0];
            Vector3 lastPosition = lastTransform.position;
            Vector3 initialPosition = lastPosition;
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(lastPosition, 0.2f);
            for(int i = 1; i< patrolPoints.Length; i++){
                Vector3 aux1 = patrolPoints[i].position;
                Gizmos.color = Color.black;
                Gizmos.DrawLine(lastPosition, aux1);
                
                lastPosition = aux1;
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(aux1, 0.2f);

            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(initialPosition, lastPosition);
            

        }
    }
}
