using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackState : AiState
{
    private Animator animator;
    AnimatorStateInfo stateInfo;
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
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if(!((stateInfo.IsName("Attack") || stateInfo.IsName("EnemyGetHit")) && stateInfo.normalizedTime <= 1f)){
            if(distanceToPlayer > agent.config.stoppingDistance){
                if(agent.sensor.Objects.Count > 0)
                    agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
                else
                    agent.stateMachine.ChangeState(AiStateId.LookForPlayer); 
            }else{
                agent.animator.SetTrigger("Attack");
            }
                
        }

    }
}
