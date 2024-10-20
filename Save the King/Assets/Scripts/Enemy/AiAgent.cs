using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public bool drawAlertState;
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Transform playerTransform;
    public AiStateId currentState;
    public AiSensor sensor;
    public float alertState = 0;
    public float alertRate = 0;
    public float maxHealth;
    public float currentHealth;
    public Transform[] patrolPoints;  // Array of waypoints 
    public bool drawPatrol;
    private float targetSpeed;
    private float currentSpeed;


    CinemachineImpulseSource screenShake;
    float powerAmount;
    Animator animator;
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
        sensor = GetComponent<AiSensor>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent.speed = config.speed;
        navMeshAgent.stoppingDistance = config.stoppingDistance;
        alertRate = config.alertRate;
        powerAmount = config.powerAmount;

        screenShake = GetComponent<CinemachineImpulseSource>();
        collid = GetComponent<Collider>();

        
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiLookForPlayerState());
        stateMachine.ChangeState(initialState);
        
    }

    void Update()
    {
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
            float distanceFactor = (config.maxSightDistance/3)/distanceToPlayer;
            alertState += alertRate * Time.deltaTime * distanceFactor;
        }else{
            if(canChangeState){
                ScreenShake((transform.position - sensor.Objects[0].transform.position).normalized);    
                stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }

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
        currentHealth -= amount;
        healthBar.SetHealthBarPercentage(currentHealth/maxHealth);

        if(currentHealth > 0){
            animator.Play("EnemyGetHit",0,0f);        }
        if(currentHealth <= 0.0f){
            animator.Play("FallingDeath");
            healthBar.gameObject.SetActive(false);
            collid.enabled = false;
            sensor.enabled = false;
            stateMachine.ChangeState(AiStateId.Death);
        }
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
            lastPosition.y = 0;
            Vector3 initialPosition = lastPosition;
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(lastPosition, 0.2f);
            for(int i = 1; i< patrolPoints.Length; i++){
                Vector3 aux1 = patrolPoints[i].position;
                aux1.y = 0; 
                Gizmos.color = Color.black;
                Gizmos.DrawLine(lastPosition, aux1);
                
                lastPosition = aux1;
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(aux1, 0.2f);

            }

            Gizmos.color = Color.black;
            Gizmos.DrawLine(initialPosition, lastPosition);
            

        }
    }
}
