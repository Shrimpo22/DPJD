using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OpenableInteractable : Interactable
{
    public IOpenable openable;
    public bool available;
    private string original_text;

    public override void Start(){
        openable = GetComponent<IOpenable>();
        if(interactCanvas != null){
            interactCanvas.gameObject.SetActive(false);
            original_text = interactCanvas.GetComponentInChildren<TMP_Text>().text;
        }
    }
    
    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        if(available){
            openable.Open(playerItems.gameObject, interactCanvas);
            if(!openable.IsClosed){
                playerItems.ClearClosestInteractable();
                gameObject.layer = 0;
                available = false;
            }
        }
    }

    public void ForceOpen(){
        openable.ForceOpen(GameObject.FindGameObjectWithTag("Player"), interactCanvas);
    }

    public override void EnterRange(){
        if(available){
            interactCanvas.GetComponentInChildren<TMP_Text>().text = original_text;
            interactCanvas.gameObject.SetActive(true);  
        }
    }

    public override void ExitRange()
    {
        if(available){
            base.ExitRange();
        }
    }
}
