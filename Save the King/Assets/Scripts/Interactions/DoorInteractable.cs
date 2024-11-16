using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DoorInteractable : Interactable
{
    public IDoor door;
    public bool available;

    public override void Start(){
        door = GetComponent<IDoor>();
        interactCanvas.gameObject.SetActive(false);
    }
    
    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        door.OpenDoor(playerItems.gameObject, interactCanvas);
        if(!door.IsClosed){
            playerItems.ClearClosestInteractable();
            gameObject.layer = 0;
            available = false;
        }
    }

    public override void EnterRange(){
        if(available){
            interactCanvas.GetComponentInChildren<TMP_Text>().text = "Open Door";
            interactCanvas.gameObject.SetActive(true);  
        }
    }
}
