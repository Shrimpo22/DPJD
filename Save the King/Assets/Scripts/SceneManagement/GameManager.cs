using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public GameObject playerHUD;
    
    // Store player data like position and scene state
    public Dictionary<string, SceneState> sceneStates = new();

    private void Awake()
    {
        // Registar o método no evento quando a cena for carregada
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure this persists between scenes
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(playerHUD);
   
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para teletransportar o jogador para a próxima cena
    public void TeleportToNextScene(string nextSceneName)
    {
        // Carregar a cena com base no nome passado
        SceneManager.LoadScene(nextSceneName);
    }

    // Esse método será chamado quando a nova cena for carregada
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Procurar o objeto de destino na nova cena
        GameObject targetObject = GameObject.FindGameObjectWithTag("SpawnPoint"+scene.name);;
        CharacterController controller = player.GetComponentInChildren<CharacterController>();
        if (targetObject != null)
        {
            Debug.Log("SPAWN POSITION : " + targetObject.transform.position);
            Debug.Log("Player POSITION : " + player.transform.position);
            // Teletransportar o jogador para a posição do objeto
            controller.enabled = false; // Disable temporarily to prevent conflicts
            controller.transform.position = targetObject.transform.position;
            //player.transform.position = targetObject.transform.position;
            controller.enabled = true;
        }
    }

     private void OnDestroy()
    {
        // Desregistrar o evento ao destruir o objeto para evitar problemas
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }





    public void SaveCurrentSceneState()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneState state = new()
        {
            playerLastPosition = player.transform,
            // Add more state data as needed
            //playerGO = player, // save player gameobject;
            // inventory
            // enemies dead
            // puzzles done
        };
        Instance.sceneStates[currentScene] = state;
    }

    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        CharacterController controller = player.GetComponentInChildren<CharacterController>();

        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint"+sceneName);
        Debug.Log("spawnPoint : " + spawnPoint);
        
        if (spawnPoint != null) {
            Debug.Log("SPAWN POINT : " + spawnPoint.transform.position);
            Vector3 spawnPosition = spawnPoint.transform.position;
            Debug.Log("SPAWN POSITION : " + spawnPosition);
            Debug.Log("CONTROLLER POSITION : " + controller.transform.position);
            controller.enabled = false; // Disable temporarily to prevent conflicts
            player.transform.position = spawnPosition;
            controller.enabled = true;
        
        } else {
            controller.enabled = false; // Disable temporarily to prevent conflicts
            player.transform.position = new Vector3(0,0,0);
            controller.enabled = true;
        }

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

