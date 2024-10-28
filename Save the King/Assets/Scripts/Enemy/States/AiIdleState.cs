using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class AiIdleState : AiState
{

    private Transform[] patrolPoints;  // Array of waypoints 
    private float patrolTimer = 0f;
    private float patrolInterval = 0f;
    private float patrolSpeed = 0f;
    private int currentPatrolIndex = 0;

    private float lookAroundSpeed = 2.0f;  // Speed for rotating to look around
    private float lookAroundAngle = 45.0f;
    private float lookChangeInterval; // Time between switching look directions
    private float lookChangeTimer = 0f; 
    private float lookDirection = 1.0f;    // Direction to rotate (-1 for left, 1 for right)
    private Quaternion targetRotation;
    private bool hasRotated;
    public void Enter(AiAgent agent)
    {
        currentPatrolIndex = 1;
        patrolInterval = agent.config.patrolInterval;
        patrolSpeed = agent.config.patrolSpeed;
        patrolPoints = agent.patrolPoints;
        int randomNumber = Random.Range(2, 6);
        lookChangeInterval = patrolInterval/randomNumber;
        hasRotated = false;
    }

    public void Exit(AiAgent agent)
    {
    }

    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }

    public void Update(AiAgent agent)
    {
        CheckForPlayer(agent);

        if(!agent.navMeshAgent.hasPath){
            patrolTimer += Time.deltaTime;
            if(patrolTimer >= patrolInterval){
                GoToNextPatrol(agent);
                patrolTimer = 0f;
            }else{
                LookAround(agent);
            }
        }
    }

    private void CheckForPlayer(AiAgent agent){
        if (agent.sensor){
            if(agent.sensor.Objects.Count > 0)
                agent.IncreaseAlertState();
            else{
                agent.DecreaseAlertState();
            }
        }
    }

    void GoToNextPatrol(AiAgent agent){
        if(patrolPoints.Length<=1) return;

        agent.navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;

        agent.navMeshAgent.speed = patrolSpeed;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        targetRotation = agent.transform.rotation;
        hasRotated = false;
        lookChangeTimer = 0f;
    }

    void LookAround(AiAgent agent){
        if (lookChangeTimer >= lookChangeInterval)
        {                
            lookDirection *= -1;  // Switch direction
            lookChangeTimer = 0f; // Reset timer
            float targetAngle = lookDirection * lookAroundAngle;
            if(hasRotated){
                targetAngle *= 2;
            }
            targetRotation = agent.transform.rotation * Quaternion.Euler(0, targetAngle, 0);
            hasRotated = true;
        }
        if(hasRotated){
            agent.transform.rotation = Quaternion.Slerp(
            agent.transform.rotation, 
            targetRotation * Quaternion.Euler(0,0,0), 
            Time.deltaTime * lookAroundSpeed
            );
        }
        lookChangeTimer += Time.deltaTime;
    }

}
