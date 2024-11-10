using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Food : MonoBehaviour
{
    private bool hasBeenGrabed = false; 
    public TMP_Text textMapPiece;
    // Start is called before the first frame update
    void Start(){
        textMapPiece.color = Color.black;
        textClear();
    }
    
    public void textActivate(){
      textMapPiece.text  = "(E) Eat Food";
      textMapPiece.gameObject.SetActive(true);  
    }

    public void textClear(){
      textMapPiece.gameObject.SetActive(false);  
    }

    public void grabItem(GameObject player) {
            if(!hasBeenGrabed){
              float current = player.GetComponent<PlayerHealth>().currentHealth;
              float max = player.GetComponent<PlayerHealth>().maxHealth;
              if(current != max){
                  if(current+10 > max){
                    player.GetComponent<PlayerHealth>().currentHealth = max; 
                  }else{
                    player.GetComponent<PlayerHealth>().currentHealth+=10;
                  }
                  hasBeenGrabed = true;
                  Debug.Log("Adicionei");
                  Destroy(this.gameObject);
              }
            }
          
   
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }
}
