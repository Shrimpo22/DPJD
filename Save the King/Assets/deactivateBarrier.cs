using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivateBarrier : MonoBehaviour
{
    public GameObject barrier;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            barrier.GetComponent<Collider>().isTrigger = true;
        }
    }
}
