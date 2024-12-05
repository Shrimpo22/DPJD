using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public GameObject playerHUD;

    // Store player data like position and scene state
    public Dictionary<string, SceneState> savedSceneStates  = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(playerHUD);
   
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveSceneState(string sceneName, SceneState sceneState)
    {
        savedSceneStates[sceneName] = sceneState;
    }

    public SceneState LoadSceneState(string sceneName)
    {
        if (savedSceneStates.ContainsKey(sceneName))
        {
            return savedSceneStates[sceneName];
        }
        return null;
    }

}

