using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour
{
    public TMP_Text textKey;
    private bool hasBeenGrabbed = false;
    [SerializeField] KeyType keyType;
    public AudioClip grabSound;
    private AudioSource audioSource;
    public enum KeyType{
        None,
        KeyOfCell,
        KeyOfKingThrone,
        KeyOfGeneralRoom2,
        KeyOfFeastRoom,
        KeyOfChest,
        KeyOfKitchen,
        KeyOfDungeon,
        KeyOfWardrobe

    }

    void Start(){
        textKey.color = Color.white;
        audioSource = GetComponent<AudioSource>();
    }
    
    private Key.KeyType getKeyType(){
        return keyType;
    }


    public void textActivate(){
      textKey.text  = "(E) Grab Key";
      textKey.gameObject.SetActive(true);  
    }

    public void textClear(){
      textKey.gameObject.SetActive(false);  
    }

    public void grabKey(GameObject player) {
        if(!hasBeenGrabbed){
            textClear();
            hasBeenGrabbed=true;
            audioSource.clip = grabSound;
            audioSource.Play();
            player.GetComponent<PlayerEventItens>().AddKey(keyType);
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            inventory.GetComponent<Inventory>().AddItem(keyType.ToString(),1);
            Destroy(transform.parent.gameObject,grabSound.length);
        }
   
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }


}
