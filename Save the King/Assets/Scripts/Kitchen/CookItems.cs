using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CookItems : MonoBehaviour
{
    private bool hasBeenGrabed = false; 
    public TMP_Text CookText;
    void Start(){
        CookText.color = Color.black;
        textClear();
    }
    
    public void textActivate(){
      CookText.text  = "(E) Grab Vase";
      CookText.gameObject.SetActive(true);  
    }

    public void textClear(){
      CookText.gameObject.SetActive(false);  
    }

    public void grabItem(GameObject player) {
            if(!hasBeenGrabed){
              hasBeenGrabed = true;
              Debug.Log("Adicionei");
              string itemName = this.gameObject.name;
              GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
              inventory.GetComponent<Inventory>().AddItem(itemName,1);
              Destroy(this.gameObject);
              
            }
            
            
   
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }
}
