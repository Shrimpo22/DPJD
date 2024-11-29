using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;
    public GameObject inventory;
    
    // Store player data like position and scene state
    public Dictionary<string, SceneState> sceneStates = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure this persists between scenes
            DontDestroyOnLoad(player);
   
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SaveCurrentSceneState()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneState state = new()
        {
            playerLastPosition = player.transform
            // Add more state data as needed
            // inventory
            // enemies dead
            // puzzles done
        };
        Instance.sceneStates[currentScene] = state;
    }

    public IEnumerator LoadNewScene(string sceneName, Vector3 playerSpawnPosition)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // After scene load, position the player at the spawn point
        transform.position = playerSpawnPosition;

        // If this scene was visited before, restore its state
        if (Instance.sceneStates.ContainsKey(sceneName))
        {

            RestoreSceneState(sceneName);
            Debug.Log("restored");
            // Restore more state data as needed
        }
    }

    void RestoreSceneState(string sceneName)
    {
        if (Instance.sceneStates.ContainsKey(sceneName))
        {
            SceneState state = Instance.sceneStates[sceneName];
            transform.position = state.playerLastPosition.position;
        }
    }

}

