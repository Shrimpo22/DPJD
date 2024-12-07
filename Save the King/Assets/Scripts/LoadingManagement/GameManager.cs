using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject playerHUD;
    public Transform respawnPoint;
    public EnemyManager currentEnemyManager;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(playerHUD);
   
        }
        else
        {
            Destroy(gameObject);
        }
    }



}

