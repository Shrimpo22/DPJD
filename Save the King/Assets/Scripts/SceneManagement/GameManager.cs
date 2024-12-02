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
       // SceneManager.sceneLoaded += OnSceneLoaded;
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

    // Método para teletransportar o jogador para a próxima cena
    public void TeleportToNextScene(string nextSceneName)
    {
        // Carregar a cena com base no nome passado
        SceneManager.LoadScene(nextSceneName);
    }

    // Esse método será chamado quando a nova cena for carregada
    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
   //     // Procurar o objeto de destino na nova cena
    //    GameObject targetObject = GameObject.FindGameObjectWithTag("SpawnPoint"+scene.name);;
    //    CharacterController controller = player.GetComponentInChildren<CharacterController>();
    //    if (targetObject != null)
    //    {
    //        Debug.Log("SPAWN POSITION : " + targetObject.transform.position);
    //        Debug.Log("Player POSITION : " + player.transform.position);
    //        // Teletransportar o jogador para a posição do objeto
    //        controller.enabled = false; // Disable temporarily to prevent conflicts
    //        controller.transform.position = targetObject.transform.position;
    //        //player.transform.position = targetObject.transform.position;
    //        controller.enabled = true;
    //    }
   // }

    // private void OnDestroy()
    //{
    //    // Desregistrar o evento ao destruir o objeto para evitar problemas
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}



}

