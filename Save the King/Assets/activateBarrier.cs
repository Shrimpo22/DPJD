using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class activateBarrier : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
