        using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EnemyManager;

public class SceneLoadTrigger : MonoBehaviour
{

    [SerializeField] private GameObject[] _roomsToLoad;
    [SerializeField] private GameObject[] _roomsToUnload;

    private GameObject _player;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider collision)
    {   
        if (collision.gameObject == _player)
        {
            LoadRooms();
            UnloadRooms();
        }
    }

    private void LoadRooms()
    {
        foreach(var room in _roomsToLoad)
        {
            room.SetActive(true);
            // Assuming the GameManager is attached somewhere in the children
            EnemyManager enemyManager = room.GetComponentInChildren<EnemyManager>();
            if (enemyManager != null)
            {
                // Loop through enemies in the GameManager and check if they are dead
                foreach (EnemyData enemy in enemyManager.enemies)
                {
                    if (enemy.enemyObject.GetComponent<AiAgent>().currentState == AiStateId.Death)
                    {
                        enemy.enemyObject.SetActive(false); // Deactivate dead enemies
                    }
                }
            }
        }
    }

    private void UnloadRooms()
    {
        foreach(var room in _roomsToUnload)
        {
           room.SetActive(false);
        }
    }
}
