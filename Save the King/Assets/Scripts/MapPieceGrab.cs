using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapPieceGrab : MonoBehaviour
{
    public TMP_Text textMapPiece;
     
    private bool hasBeenGrabbed = false;
    public AudioClip grabSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start(){
        textMapPiece.color = Color.black;
        textClear();
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
      if(!hasBeenGrabbed){
            textClear();
            hasBeenGrabbed=true;
            audioSource.clip = grabSound;
            audioSource.Play();
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            inventory.GetComponent<Inventory>().AddItem("MapPiece",1);
            Destroy(this.gameObject,grabSound.length);
      }
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }
}
