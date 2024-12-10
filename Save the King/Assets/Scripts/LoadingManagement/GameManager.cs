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
                        if (objA.gameObject.name == objB.gameObject.name) // Comparar os nomes dos objetos
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

    public void RespawnNewPickablesActivate(List<GameObject> listToSpawn, List<GameObject> listWithMissingObjects)
    {
        List<GameObject> toSpawn = getMissingObjects(listToSpawn, listWithMissingObjects);  // Copy the list to avoid modifying the original

        // Activate remaining objects in the list
        foreach (GameObject objToSpawn in toSpawn)
        {
            objToSpawn.SetActive(true);
        }
    }

    public List<GameObject> getMissingObjects(List<GameObject> listToSpawn, List<GameObject> listWithMissingObjects)
    {
         List<GameObject> toSpawn = new(listToSpawn);  // Copy the list to avoid modifying the original

        // Iterate in reverse to safely remove elements from the list
        for (int i = toSpawn.Count - 1; i >= 0; i--)
        {
            GameObject objToSpawn = toSpawn[i];
            if (objToSpawn != null)
            {
                foreach (GameObject missingObject in listWithMissingObjects)
                {
                    if (missingObject != null)
                    {
                        if (objToSpawn.transform.position == missingObject.transform.position)
                        {
                            toSpawn.RemoveAt(i);  // Safely remove by index
                            break;  // Exit the inner loop after removing
                        }
                    }
                }
            }
        }

        return toSpawn;
    }
}

