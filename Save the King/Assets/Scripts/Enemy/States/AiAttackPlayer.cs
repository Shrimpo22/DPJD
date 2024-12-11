using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AiAttackState : AiState
{
    private Animator animator;
    float distanceToPlayer;
    bool ishalfLife = false;
    
    public void Enter(AiAgent agent)
    {
        animator = agent.animator;
    }

    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = 0;
    }

    public AiStateId GetId()
    {
        return AiStateId.AttackState;
    }

    public void Update(AiAgent agent)
    {
        agent.transform.LookAt(agent.playerTransform.position);
        distanceToPlayer = (agent.transform.position - agent.playerTransform.position).magnitude;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo stateInfoNext = animator.GetNextAnimatorStateInfo(0);

        if(stateInfo.IsName("Attack") && distanceToPlayer < agent.config.stoppingDistance)
        {
            agent.navMeshAgent.speed = 0f;
            animator.SetTrigger("Attack");
            
        }
        if(!(stateInfo.IsName("EnemyGetHit") || stateInfo.IsName("Attack") || stateInfo.IsName("throw")) && !(stateInfoNext.IsName("EnemyGetHit") || stateInfoNext.IsName("Attack") || stateInfoNext.IsName("throw"))){
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Throw");

            if(distanceToPlayer > agent.config.stoppingDistance && !(stateInfo.IsName("EnemyGetHit") || stateInfo.IsName("Attack"))){
                agent.navMeshAgent.speed = agent.config.speed;
                if (agent.isBoss)
                {
                    if (stateInfo.IsName("Movement"))
                    {
                        agent.navMeshAgent.destination = agent.playerTransform.position;
                        float elapsedTime = stateInfo.normalizedTime * 3;
                        if (elapsedTime >= 3f)
                        {
                            ChooseAttack(agent);
                        }

                    }
                    
                }
                else { 
                if(agent.sensor.Objects.Count > 0)
                    agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
                else
                    agent.stateMachine.ChangeState(AiStateId.LookForPlayer);
                }
            }
            else{
                agent.navMeshAgent.speed = 0;
                animator.SetTrigger("Attack");
            }       
        }else{
            agent.navMeshAgent.speed = 0;
        }
    }

    public void ChooseAttack(AiAgent agent)
    {

        float random = Random.Range(0f, 1f);
        if (agent.isBoss)
        {
            if (random < 0.9f)
            {
                agent.navMeshAgent.destination = agent.playerTransform.position;
            }
            if (agent.gameObject.GetComponent<BossAttackTrap>().activeTraps.Count <= 4 && random >= 0.9f)
            { 
                animator.SetTrigger("Throw");
            }
            else 
            {
                agent.navMeshAgent.speed = agent.config.speed;
                agent.navMeshAgent.destination = agent.playerTransform.position;
            }
        }
    }
}
