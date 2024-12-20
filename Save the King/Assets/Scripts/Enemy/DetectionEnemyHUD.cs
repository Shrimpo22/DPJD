using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public AiAgent agent;
    public Transform enemyCenter;

    // Events to notify player detection system
    public delegate void DetectionEvent(EnemyDetection enemy);
    public static event DetectionEvent OnDetectPlayer;
    public static event DetectionEvent OnLosePlayer;
    public static event DetectionEvent OnChasePlayer;
    public static event DetectionEvent OnLook4Player;
    public static event DetectionEvent OnIncreaseCounter;
    public static event DetectionEvent OnDecreaseCounter;
    private bool playerDetected = false;  // Tracks if player detection state has changed
    private bool playerLost = true;
    private AiStateId previousState;  // Tracks the previous AI state
    

    void Update()
    {
        // Notify if detection status changes
        if (agent.alertState > 0.0f && !playerDetected){
            OnDetectPlayer?.Invoke(this);
            playerDetected = true;  // Set the detection flag
            playerLost = false;  // Reset the lost flag  // Enemy starts detecting the player
        }else if (agent.alertState <= 0.0f && !playerLost ){
            playerDetected = false;  // Reset the detection flag
            playerLost = true;  // Set the lost flag
            OnLosePlayer?.Invoke(this);  // Enemy stops detecting the player
        }
        if (agent.currentState != previousState) {
            // State has changed, check which event to trigger
            if (agent.currentState == AiStateId.ChasePlayer) {
                OnIncreaseCounter?.Invoke(this);
                OnChasePlayer?.Invoke(this);
            } 
            else if (agent.currentState == AiStateId.LookForPlayer) {
                OnDecreaseCounter?.Invoke(this);
                OnLook4Player?.Invoke(this);
            }else if(agent.currentState == AiStateId.Death){
                OnDecreaseCounter?.Invoke(this);
                OnLosePlayer?.Invoke(this);
            }

        // Update the previous state tracker
            previousState = agent.currentState;
        }
    }
}

