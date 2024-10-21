using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] public TMP_Text textKey;
    [SerializeField] KeyType keyType;
    private AudioSource audioSource;
    public enum KeyType{
        None,
        KeyOfCell,
        KeyOfKingThrone,
        KeyOfGeneralRoom2,
        KeyOfFeastRoom,

    }

    void Start(){
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

    private TMP_Text textOfKey;
    public void grabKey(GameObject player) {
        Debug.Log("Im in the key");
        if (Input.GetKeyDown(KeyCode.E)){
            player.GetComponent<PlayerEventItens>().AddKey(keyType);
            
            player.GetComponent<PlayerEventItens>().isNearKey = false;
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            inventory.GetComponent<Inventory>().AddItem(keyType.ToString(),1);
            Destroy(this.gameObject);

        }   
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }


}
