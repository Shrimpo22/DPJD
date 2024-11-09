using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Food : MonoBehaviour
{
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
            //Adicionar linha da barra de vida do player
            
            Destroy(this.gameObject);
   
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }
}
