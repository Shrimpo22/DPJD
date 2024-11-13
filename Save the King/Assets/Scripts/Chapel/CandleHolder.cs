using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CandleHolder : MonoBehaviour
{
    public TMP_Text textEvent;

    public int nrOfPiecesOn;
    private int totalPieces = 2;
    private bool isComplete = false;

    public bool isLooking = false;
    GameObject inventory;
    GameObject player;
    public Camera myCamera;
    private Camera mainCamera;

    [SerializeField] public GameObject[] allPieces;
    
    void Start()
    {
       nrOfPiecesOn = 0 ;
       player = GameObject.FindGameObjectWithTag("Player");
       inventory = GameObject.FindGameObjectWithTag("Inventory");
       mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void textActivate(){
      textEvent.text  = "(E) Interact";
      textEvent.gameObject.SetActive(true);  
    }
    public void textClear(){
      textEvent.gameObject.SetActive(false);  
    }
    public void seeObject() {
        mainCamera.tag="Untagged";
        myCamera.tag="MainCamera";
        player.SetActive(false);
        isLooking = true;
        mainCamera.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(true);
         textEvent.color = Color.black;
         if(!isComplete){
        textEvent.text  = "(E) Add Piece ; (ESC) Exit";
       } else{
        textEvent.text = "(ESC) Exit";}
        isLooking=true;

    }

    private void showFinalMap(){
        isComplete=true;
    }

    public void addPiece(){
       inventory.GetComponent<Inventory>().DropItemByName("Candles");
       allPieces[nrOfPiecesOn].SetActive(false);
       nrOfPiecesOn++;
    }
    private void OnTriggerExit(Collider other){
        textClear();
    }
    void Update()
    {
       if(nrOfPiecesOn == totalPieces && !isComplete) {
            textEvent.text = "(ESC) Exit";
            showFinalMap();
       }

       if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
            player.SetActive(true);
            textClear();
            mainCamera.tag = "MainCamera";
            myCamera.tag="Untagged";
            mainCamera.gameObject.SetActive(true);
            myCamera.gameObject.SetActive(false);
            isLooking = false;
            inventory.GetComponent<Inventory>().isLookingAtMap = false;
            inventory.GetComponent<Inventory>().InventoryMenu.SetActive(false);

           
        }else if(Input.GetKeyDown(KeyCode.E) && isLooking && !isComplete){
                inventory.GetComponent<Inventory>().OpenIt();
                
        }
    }
}
