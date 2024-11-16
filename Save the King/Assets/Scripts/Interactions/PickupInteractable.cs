using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupInteractable : Interactable
{
    public ItemType itemToGive;
    public int amount;
    private bool pickedUp = false;
    public bool clickable = false;
    public AudioSource audioSource;
    public AudioClip pickupSound;
    public override void Start(){
        interactCanvas.gameObject.SetActive(false);
        if(audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        if(pickupSound == null)
            pickupSound = Resources.Load<AudioClip>("Sounds/Puma");
    }
    
    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        if(!pickedUp){
            pickedUp = true;
            inv.AddItem(itemToGive.ToString(), amount);
            interactCanvas.gameObject.SetActive(false);
            if(pickupSound != null){
                audioSource.clip = pickupSound;
                audioSource.Play();
                ClearAndDestroy(playerItems, pickupSound.length);
            }else{
                ClearAndDestroy(playerItems, 0);
            }

        }
    }

    public void OnMouseDown(){
        if(clickable){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Interact(player.GetComponent<Inventory>(), player.GetComponent<PlayerEventItens>());
        }   
    }
}
