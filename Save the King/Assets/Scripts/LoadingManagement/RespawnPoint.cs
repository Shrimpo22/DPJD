using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] public List<GameObject> roomObjects; // List of objects in the room to track
    [SerializeField] public List<GameObject> pickablesToSave; // List of objects in the room to track
    public List<GameObject> pickablesToSpawn;
    public List<GameObject> pickablesToSpawn2;
    public bool canBringSword = true;
    public bool firstCollision = true;
    public bool initialDone = false;


    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider collision)
    {   
        if (collision.gameObject == _player)
        {
            if ( GameManager.instance.respawnPoint != null && GameManager.instance.respawnPoint != gameObject.transform)
            {
                Debug.Log("Before respawn point : " + GameManager.instance.respawnPoint);
                Destroy(GameManager.instance.respawnPoint.gameObject);
            }
            GameManager.instance.respawnPoint = gameObject.transform;
            Debug.Log("After respawn point : " + GameManager.instance.respawnPoint);
            EnemyManager currentEnemyManager = gameObject.transform.root.GetComponentInChildren<EnemyManager>();
            GameManager.instance.currentEnemyManager = currentEnemyManager;

            SaveRoomState(); // Save initial state of room objects like doors, etc.
            if (transform.parent.gameObject.name == "Masmorra" && firstCollision)
            {
                canBringSword = false;
            }
            firstCollision = false;
        }
        
    }

     // Save the initial state of the room (e.g., doors, switches)
    private void SaveRoomState()
    {   
        Debug.Log("saving room state...");
        foreach (GameObject obj in roomObjects)
        {
            if (obj != null)
            {
                RoomObjectState roomObjectState = obj.GetComponent<RoomObjectState>();
                Debug.Log(roomObjectState);
                if (roomObjectState != null)
                {
                    roomObjectState.SaveInitialState();
                }
            }
        }
        if(!initialDone)
        {
            SavePickablesInitial();
        }
        pickablesToSpawn2.RemoveAll(item => true);
        SavePickablesRecursive();

    }

    private void SavePickablesRecursive()
    {
        foreach(GameObject obj in pickablesToSpawn)
        {
            GameObject pickable = Instantiate(obj, obj.transform.position, obj.transform.rotation);
            pickable.SetActive(false);
            pickablesToSpawn2.Add(pickable);
        }
    }

    private void SavePickablesInitial()
    {
        foreach(GameObject obj in pickablesToSave)
        {
            if (obj != null) {
                GameObject pickable = Instantiate(obj, obj.transform.position, obj.transform.rotation);
                pickable.SetActive(false);
                pickablesToSpawn.Add(pickable);
            }
        }
        initialDone = true;
    }
    
}
