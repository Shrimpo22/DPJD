using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointGizmos : MonoBehaviour
{
    void Awake(){
        if(transform.parent.parent.name != "PatrolPoints")
            transform.parent.parent = null;
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}
