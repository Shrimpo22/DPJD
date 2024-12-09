using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private AiAgent agent;

    public GameObject item;

    void Start(){
        agent = gameObject.GetComponent<AiAgent>();
    }
    void Update()
    {
        if(agent.currentState == AiStateId.Death){
            item.transform.position = agent.transform.position;
            item.SetActive(true);
        }
    }
}
