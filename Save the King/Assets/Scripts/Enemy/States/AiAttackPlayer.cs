using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackState : AiState
{
    private Animator animator;
    float distanceToPlayer;
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
            ChooseAttack();
            
        }
        if(!(stateInfo.IsName("EnemyGetHit") || stateInfo.IsName("Attack")) && !(stateInfoNext.IsName("EnemyGetHit") || stateInfoNext.IsName("Attack"))){
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Throw");

            if(distanceToPlayer > agent.config.stoppingDistance && !(stateInfo.IsName("EnemyGetHit") || stateInfo.IsName("Attack"))){
                agent.navMeshAgent.speed = agent.config.speed;
                if(agent.sensor.Objects.Count > 0)
                    agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
                else
                    agent.stateMachine.ChangeState(AiStateId.LookForPlayer); 
            }else{
                agent.navMeshAgent.speed = 0;
                ChooseAttack();
            }       
        }else{
            agent.navMeshAgent.speed = 0;
        }
    }

    public void ChooseAttack()
    {
        float random = Random.Range(0f, 1f);
        if (random < 0.1)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.SetTrigger("Throw");
        }
    }
}
