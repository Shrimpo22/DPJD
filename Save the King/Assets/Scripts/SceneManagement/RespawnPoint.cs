using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private GameObject _player;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider collision)
    {   
        if (collision.gameObject == _player)
        {
            GameManager.instance.respawnPoint = gameObject.transform;
            EnemyManager currentEnemyManager = gameObject.transform.root.GetComponentInChildren<EnemyManager>();
            GameManager.instance.currentEnemyManager = currentEnemyManager;
            //Debug.Log("COLLISION, ENEMYMANAGER : " + gameObject.transform.root.GetComponentInChildren<EnemyManager>());
        }
    }
}
