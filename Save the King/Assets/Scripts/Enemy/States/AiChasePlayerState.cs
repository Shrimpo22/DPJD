using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{

    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = agent.config.stoppingDistance;
    }

    public void Exit(AiAgent agent)
    {
    }

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Update(AiAgent agent)
    {
        if (agent.sensor.Objects.Count > 0){
            if((agent.transform.position - agent.sensor.Objects[0].transform.position).magnitude <= agent.config.stoppingDistance){
                agent.stateMachine.ChangeState(AiStateId.AttackState);
            }else
                agent.navMeshAgent.destination = agent.sensor.Objects[0].transform.position;
        }else{
            agent.navMeshAgent.stoppingDistance = 0;
            if(!agent.navMeshAgent.hasPath)
                agent.stateMachine.ChangeState(AiStateId.LookForPlayer);
        }
    }
}
