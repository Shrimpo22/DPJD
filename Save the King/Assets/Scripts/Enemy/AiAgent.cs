using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    public GameObject toDestroy;
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
    public bool isBoss = false;

    [SerializeField] UIHealthBar healthBar;
    private bool isHalfLife = false;


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
        if(!gameObject.activeSelf){
            gameObject.SetActive(true);
        }
        Debug.Log("[Ghost] where am at " + gameObject.name + " " + gameObject.activeSelf);

        if (isNpc) return;
        stateMachine.Update();
        if (navMeshAgent.hasPath)
        {
            targetSpeed = navMeshAgent.speed;
        }
        else
        {
            targetSpeed = 0;
        }

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 4f);

        animator.SetFloat("speed", currentSpeed);
    }

    public void IncreaseAlertState()
    {
        if (alertState < 1)
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
            float distanceFactor;
            if (distanceToPlayer < 3f)
            {
                distanceFactor = Mathf.Exp(-0.3f * distanceToPlayer) * 20;
            }
            else
            {
                distanceFactor = Mathf.Exp(-0.3f * distanceToPlayer) * 8;
            }
            if (playerTransform.GetComponent<PlayerMovement>().isCrouching)
                distanceFactor /= 2;
            alertState += alertRate * Time.deltaTime * distanceFactor;
        }
        else
        {
            if (canChangeState)
            {
                ScreenShake((transform.position - sensor.Objects[0].transform.position).normalized);
                stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }
    }

    public void SetAlertState(float amount)
    {
        alertState = amount;
    }
    public void ScreenShake(Vector3 dir)
    {
        Debug.Log("ScreenShake made?");
        screenShake.GenerateImpulseWithVelocity(dir);
    }
    public void DecreaseAlertState()
    {
        if (alertState > 0)
            alertState -= alertRate * Time.deltaTime;
        if (alertState < alertRate)
            alertState = 0;
    }
    public void TakeDamage(float amount)
    {
        Debug.Log("[Enemy Ghost] Taking DMG" + gameObject.name + " " + gameObject.activeSelf);
        if (isNpc) return;
        currentHealth -= amount;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);

        if (currentHealth > 0)
        {
            if (isBoss && currentHealth <= maxHealth / 2 && !isHalfLife)
            {
                GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>().SetGameState(MusicManager.GameState.Boss2);
                isHalfLife = true;
                animator.Play("SecondPhase");
                gameObject.GetComponent<EnemyAnimSoundEvents>().BossGrunt();
                return;
            }

            if (isBoss && currentHealth <= maxHealth / 2)
            {
                GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>().SetGameState(MusicManager.GameState.Boss2);
                gameObject.GetComponent<EnemyAnimSoundEvents>().EnemyGetHit();
                animator.Play("EnemyGetHit", 0, 0.6f);
                SetAlertState(1.2f);
                stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
            else
            {
                animator.Play("EnemyGetHit", 0, 0f);
                SetAlertState(1.2f);
                stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }
        if (currentHealth <= 0.0f)
        {
            animator.Play("FallingDeath");
            healthBar.gameObject.SetActive(false);
            collid.enabled = false;
            if (sensor != null)
                sensor.enabled = false;
            stateMachine.ChangeState(AiStateId.Death);
            if (isBoss)
            {
                StartCoroutine(FinalCutscene());
            }
        }
        Debug.Log("[Enemy Ghost] Taking DMG 2" + gameObject.name + " " + gameObject.activeSelf);

    }
    private IEnumerator FinalCutscene()
    {
        yield return new WaitForSeconds(2f);
        Destroy(toDestroy);
        SceneManager.LoadScene("FinalCutscene");
    }

    public void Reset()
    {
        isHalfLife = false;
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.SetHealthBarPercentage(1);
        }
        EnemyRecipeInteractable npc = gameObject.GetComponent<EnemyRecipeInteractable>();
        if (npc != null)
        {
            isNpc = true;
            npc.usedItem = false;
            npc.fighting = false;
        }
        collid.enabled = true;
        if (sensor != null)
            sensor.enabled = true;
        SetAlertState(0);
        stateMachine.ChangeState(initialState);
        animator.Play("Movement");
    }

    public void EnableCollider()
    {
        if (weapon != null)
            weapon.EnableCollider();
    }
    public void DisableCollider()
    {
        if (weapon != null)
            weapon.DisableCollider();
    }

    public void ClearHits()
    {
        weapon.RemoveHitTargets();
    }

    public void EnableInvulnerability()
    {
        this.tag = "Untagged";
    }

    public void DisableInvulnerability()
    {
        this.tag = "Target";
    }

    void OnDrawGizmos()
    {
        if (drawAlertState)
        {
            Gizmos.color = Color.white;
            if (alertState > 0 && alertState <= 0.25)
                Gizmos.color = Color.yellow;
            if (alertState > 0.25 && alertState < 0.75)
                Gizmos.color = Color.magenta;
            if (alertState > 0.75 && alertState <= 1)
                Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
        if (drawPatrol && patrolPoints.Length > 0)
        {

            Transform lastTransform = patrolPoints[0];
            Vector3 lastPosition = lastTransform.position;
            Vector3 initialPosition = lastPosition;
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(lastPosition, 0.2f);
            for (int i = 1; i < patrolPoints.Length; i++)
            {
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
