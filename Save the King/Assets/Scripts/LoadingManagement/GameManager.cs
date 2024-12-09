using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject weaponLocation;
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
    
      public void RespawnNewPickables(List<GameObject> listA, List<GameObject> listB)
    {

        foreach (GameObject objA in listA)
        {
            if(objA != null)
            {
                bool existsInB = false;

                foreach (GameObject objB in listB)
                {
                     if(objB != null)
                    {
                        if (objA.name == objB.name) // Comparar os nomes dos objetos
                        {
                            existsInB = true;
                            break;
                        }
                    }
                }

                if (!existsInB)
                {

                    objA.SetActive(true);

                }
            }
        }
    }

}

