using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CookItems : MonoBehaviour
{
    public TMP_Text textMapPiece;
    void Start(){
        textMapPiece.color = Color.black;
        textClear();
    }
    
    public void textActivate(){
      textMapPiece.text  = "(E) Grab Vase";
      textMapPiece.gameObject.SetActive(true);  
    }

    public void textClear(){
      textMapPiece.gameObject.SetActive(false);  
    }

    public void grabItem(GameObject player) {
            //Adicionar linha da barra de vida do player
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            string itemName = this.gameObject.name;
            inventory.GetComponent<Inventory>().AddItem(itemName,1);
            Destroy(this.gameObject);
   
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }
}
