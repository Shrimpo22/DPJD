using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Statues : MonoBehaviour
{
    public TMP_Text textEvent;

    [SerializeField]
    public string correctPiece; // Each statue has its correct piece (torch or chalice)
    private bool isRightPiece = false;
    private bool isEmpty = true;
    private bool isComplete = false;

    public bool isLooking = false;
    GameObject inventory;

    GameObject player;
    public Camera myCamera;
    private Camera mainCamera;

    public GameObject torch;
    public GameObject chalice;
    
    void Start()
    {
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
        if(isEmpty){
            textEvent.text  = "(E) Add Piece ; (ESC) Exit";
        } else if(!isRightPiece && !isEmpty) {
            textEvent.text  = "(E) Remove Piece ; (ESC) Exit";
        } else{
        textEvent.text = "(ESC) Exit";}
        isLooking=true;

    }

    private void showFinalMap(){
        isComplete=true;
        isRightPiece=true;
    }

    public void addPiece(string name){
       inventory.GetComponent<Inventory>().DropItemByName(name);

    }
    private void OnTriggerExit(Collider other){
        textClear();
    }
    void Update()
    {
       if(isRightPiece) {
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
