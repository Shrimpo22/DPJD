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

    public void Enter(AiAgent agent)
    {
        patrolInterval = agent.config.patrolInterval;
        patrolSpeed = agent.config.patrolSpeed;
        patrolPoints = agent.patrolPoints;
        int randomNumber = Random.Range(2, 6);
        lookChangeInterval = patrolInterval/randomNumber;
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
        patrolTimer += Time.deltaTime;
        if(!agent.navMeshAgent.hasPath){
            if(patrolTimer >= patrolInterval){
                GoToNextPatrol(agent);
                patrolTimer = 0f;
            }else{
                //LookAround(agent);
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
        if(patrolPoints.Length==0) return;
        agent.navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        agent.navMeshAgent.speed = patrolSpeed;
        Debug.Log("Rot init" + agent.gameObject.transform.rotation);
        agent.gameObject.transform.rotation = patrolPoints[currentPatrolIndex].transform.rotation;
        Debug.Log("Rot finit" + agent.gameObject.transform.rotation);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void LookAround(AiAgent agent){
        Debug.Log("LookTimer: " + lookChangeTimer + " lookInterval " + lookChangeInterval);
        if (lookChangeTimer >= lookChangeInterval)
        {
            lookDirection *= -1;  // Switch direction
            lookChangeTimer = 0f; // Reset timer

            
        }

        float targetAngle = lookDirection * lookAroundAngle;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // Smoothly rotate towards the target angle
            agent.transform.rotation = Quaternion.Slerp(
            agent.transform.rotation, 
            targetRotation * Quaternion.Euler(0,0,0), 
            Time.deltaTime * lookAroundSpeed
            );
        
        lookChangeTimer += Time.deltaTime;
    }

}
