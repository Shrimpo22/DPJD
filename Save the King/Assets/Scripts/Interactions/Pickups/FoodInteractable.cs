using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodInteractable : Interactable
{   
    public int healingAmount;
    private bool pickedUp = false;
    public AudioSource audioSource;
    public AudioClip eatSound;

    public override void Start(){
        interactCanvas.gameObject.SetActive(false);
        if(audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        if(eatSound == null){
            eatSound = Resources.Load<AudioClip>("Sounds/Gato");
        }
    }
    
    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        if(!pickedUp){
            PlayerHealth playerHealth = playerItems.gameObject.GetComponent<PlayerHealth>();
            float current = playerHealth.currentHealth;
            float max = playerHealth.maxHealth;
            if(current != max){
                if(current+healingAmount> max){
                    playerHealth.currentHealth = max; 
                }else{
                    playerHealth.currentHealth+=healingAmount;
                }
                pickedUp = true;
                audioSource.clip = eatSound;
                audioSource.Play();
                ClearAndDestroy(playerItems, eatSound.length);
            }
        }
    }
}
