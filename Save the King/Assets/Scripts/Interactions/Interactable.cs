using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Canvas interactCanvas;

    public virtual void Start(){
        interactCanvas.gameObject.SetActive(false);
    }

    public virtual void Awake(){
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public virtual void ExitRange(){
        interactCanvas.gameObject.SetActive(false);  
    }

    public virtual void EnterRange(){
        Debug.Log("Pickups Here!");
        interactCanvas.gameObject.SetActive(true);  
    }

    protected void ClearAndDestroy(PlayerEventItens playerItems, float delay = 0){
        playerItems.ClearClosestInteractable();
        Destroy(gameObject, delay);
    }

    public abstract void Interact(Inventory inv, PlayerEventItens playerItems);
}
