using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateTorch : MonoBehaviour
{
    public GameObject[] gameObjectsToActivate;
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            foreach(GameObject obj in gameObjectsToActivate){
                obj.SetActive(true);
            }
            Destroy(this);
        }
    }
}
