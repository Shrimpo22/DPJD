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
    private bool playerDetected = false;  // Tracks if player detection state has changed
    private bool playerLost = true;
    private AiStateId previousState;  // Tracks the previous AI state
    

    void Update()
    {
        // Notify if detection status changes
        if (agent.alertState > 0.0f && !playerDetected){
            Debug.Log("Detected arrow!");
            OnDetectPlayer?.Invoke(this);
            playerDetected = true;  // Set the detection flag
            playerLost = false;  // Reset the lost flag  // Enemy starts detecting the player
        }else if (agent.alertState <= 0.0f && !playerLost ){
            Debug.Log("Lost!");
            playerDetected = false;  // Reset the detection flag
            playerLost = true;  // Set the lost flag
            OnLosePlayer?.Invoke(this);  // Enemy stops detecting the player
        }
        if (agent.currentState != previousState) {
            // State has changed, check which event to trigger
            if (agent.currentState == AiStateId.ChasePlayer) {
                OnChasePlayer?.Invoke(this);
            } 
            else if (agent.currentState == AiStateId.LookForPlayer) {
                OnLook4Player?.Invoke(this);
            }

        // Update the previous state tracker
            previousState = agent.currentState;
        }
    }
}

