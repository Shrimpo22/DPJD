using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapPieceGrab : MonoBehaviour
{
    public TMP_Text textMapPiece;
     
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start(){
        textMapPiece.color = Color.black;
        audioSource = GetComponent<AudioSource>();
    }
    


    public void textActivate(){
      textMapPiece.text  = "(E) Grab Map Piece";
      textMapPiece.gameObject.SetActive(true);  
    }

    public void textClear(){
      textMapPiece.gameObject.SetActive(false);  
    }

    public void grabItem(GameObject player) {
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            inventory.GetComponent<Inventory>().AddItem("MapPiece",1);
            Destroy(this.gameObject);
   
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }
}
