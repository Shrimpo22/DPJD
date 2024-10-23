using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AiLookForPlayerState : AiState
{

    private float searchDuration = 20.0f;  // Time the AI will search for the player
    private float searchTimer = 0f;        // Timer to keep track of search time
    private float searchRadius = 3f;     // Radius for small movement during search
    private float lookAroundSpeed = 2.0f;  // Speed for rotating to look around
    private float lookAroundAngle = 80.0f; // Angle to rotate when looking around
    private float lookDirection = 1.0f;    // Direction to rotate (-1 for left, 1 for right)
    private float lookChangeInterval = 4.0f; // Time between switching look directions
    private float lookChangeTimer = 4.0f;     // Timer for changing look direction
    private Vector3 randomOffset;
    private Quaternion targetRotation;
    private bool hasRotated;          // Small random offset for movement
   
     public void Enter(AiAgent agent)
    {
        lookChangeTimer = 0f;
        searchTimer = 0f;
    }

    public void Exit(AiAgent agent)
    {
    }

    public AiStateId GetId()
    {
        return AiStateId.LookForPlayer;
    }

    public void Update(AiAgent agent)
    {   
        if(agent.sensor.Objects.Count > 0){
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);  // Or another state like Idle
        }

        searchTimer += Time.deltaTime;

        // Look around by rotating the agent left and right
        LookAround(agent);

        // Occasionally move slightly within the search radius
        if (searchTimer % 5 < 0.1f)  // Move a bit every few seconds
        {
            RandomSmallMovement(agent);
        }
        
        if (searchTimer >= searchDuration)
        {
            agent.stateMachine.ChangeState(AiStateId.Idle);  // Or another state like Idle
        }
    }

    private void LookAround(AiAgent agent)
    {
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

    private void RandomSmallMovement(AiAgent agent)
    {
        // Generate a small random offset within the defined radius
        randomOffset = new Vector3(
            Random.Range(-searchRadius, searchRadius), 
            0, 
            Random.Range(-searchRadius, searchRadius)
        );

        // Move the agent slightly by adjusting its position
        Vector3 newPos = agent.transform.position + randomOffset;
        agent.navMeshAgent.SetDestination(newPos);  // Use the NavMeshAgent to move
        hasRotated = false;
        lookChangeTimer = 0f;
    }
}
