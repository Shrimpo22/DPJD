using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearDetected : MonoBehaviour
{
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter(){
        if(playerMovement != null){
            playerMovement.EnterAndClearArrows();
        }
    }
}
