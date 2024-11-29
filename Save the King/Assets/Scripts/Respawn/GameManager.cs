using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // HOW TO ADD THE RESPAWN AND ENEMY RESET
    // Create 3 empty gameobjects, gamemanager to add this script,
    //enemymanager to add the emptygameobject holding all enemies in the scene 
    //and its script and respawnpoint for the respawnpoint script.
    // Add the necessary references
    public static GameManager instance;
    public Transform respawnPoint;

    void Awake()
    {
        // Ensure only one GameManager exists and persists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to set respawn point
    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }
}
