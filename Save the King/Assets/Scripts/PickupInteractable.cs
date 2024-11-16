using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupInteractable : Interactable
{
    public string itemToGive;
    public int amount;
    private bool pickedUp = false;
    public AudioSource audioSource;
    public AudioClip pickupSound;
    public override void Start(){
        interactCanvas.gameObject.SetActive(false);
        if(audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }
    
    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        if(!pickedUp){
            pickedUp = true;
            audioSource.clip = pickupSound;
            audioSource.Play();
            inv.AddItem(itemToGive, amount);
            ClearAndDestroy(playerItems, pickupSound.length);
        }
    }
}
