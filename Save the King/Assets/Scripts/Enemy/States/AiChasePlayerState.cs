using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{

    public void Enter(AiAgent agent)
    {
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
            agent.navMeshAgent.destination = agent.sensor.Objects[0].transform.position;
        }else{
            if(!agent.navMeshAgent.hasPath)
                agent.stateMachine.ChangeState(AiStateId.LookForPlayer);
        }
    }
}
