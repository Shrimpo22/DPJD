using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour
{
    public TMP_Text textKey;
    [SerializeField] KeyType keyType;
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

    }

    void Start(){
        textKey.color = Color.black;
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
            player.GetComponent<PlayerEventItens>().AddKey(keyType);
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            inventory.GetComponent<Inventory>().AddItem(keyType.ToString(),1);
            Destroy(this.gameObject);
   
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }


}
