using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;
public class Statues : MonoBehaviour
{
    public TMP_Text textEvent;
    public GameObject button;

    [SerializeField]
    public string correctPiece; // Each statue has its correct piece (torch or chalice)
    private bool isRightPiece = false;
    private bool isEmpty = true;
    private bool isComplete = false;
    private bool hasObject = false;

    public GameObject secretDoor;

    public bool isLooking = false;
    GameObject inventory;

    GameObject player;
    private string object_name;
    public Camera myCamera;
    private Camera mainCamera;

    public GameObject chalice;
     public GameObject torch;
    
    public GameObject freelockCamara;
    private CinemachineFreeLook freeLookComponent;

    void Start()
    {
       button.SetActive(false); 
       chalice.SetActive(false);
       torch.SetActive(false);
       player = GameObject.FindGameObjectWithTag("Player");
       freeLookComponent = freelockCamara.GetComponent<CinemachineFreeLook>();
       inventory = GameObject.FindGameObjectWithTag("Inventory");
       mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
       
    }

    public void undo(){
        if(!string.IsNullOrWhiteSpace(object_name)){
            removePiece();
        }
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
        textEvent.text = "(ESC) Exit";
        }
        isLooking=true;

    }
    
    public void findtheStatue(string name){
        secretDoor.GetComponent<OpenSecretDoor>().AddItemToStatue(name);
    }

    public void addPiece(string name){
       object_name = name;
       inventory.GetComponent<Inventory>().DropItemByName(object_name);
        hasObject = true;
       if(object_name=="Chalice"){
        chalice.SetActive(true);}else{
            torch.SetActive(true);
        }
        button.SetActive(true);
        
       if (object_name == correctPiece){
        isRightPiece=true;
        secretDoor.GetComponent<OpenSecretDoor>().sum();
        if(secretDoor.GetComponent<OpenSecretDoor>().rightPieces==secretDoor.GetComponent<OpenSecretDoor>().correctPiecesNeeded){
            exitCam();
            secretDoor.GetComponent<OpenSecretDoor>().viewOpening();
        }
       }

    }

    public void removePiece(){
        chalice.SetActive(false);
        torch.SetActive(false);
        hasObject=false;
        inventory.GetComponent<Inventory>().AddItem(object_name,1);
        object_name=null;
        button.SetActive(false);
        Debug.Log(button.activeSelf);
        if(isRightPiece){
            isRightPiece=false;
            secretDoor.GetComponent<OpenSecretDoor>().subtract();
        }
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }
    void Update()
    {
       if(isRightPiece) {
            textEvent.text = "(ESC) Exit";
       }

       if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
            exitCam();
           
        }else if(Input.GetKeyDown(KeyCode.E) && isLooking && !isComplete){
               inventory.GetComponent<Inventory>().OpenIt();
               if(hasObject) button.SetActive(true);
               secretDoor.GetComponent<OpenSecretDoor>().NearMe(this.gameObject);
        }
    }

    void exitCam(){
        button.SetActive(false);
            player.SetActive(true);
            secretDoor.GetComponent<OpenSecretDoor>().NearMe(null);
            Time.timeScale=1;
            textClear();
            mainCamera.tag = "MainCamera";
            myCamera.tag="Untagged";
            mainCamera.gameObject.SetActive(true);
            myCamera.gameObject.SetActive(false);
            isLooking = false;
            inventory.GetComponent<Inventory>().isLookingAtMap = false;
            inventory.GetComponent<Inventory>().InventoryMenu.SetActive(false);
            if (freeLookComponent != null)
            {
                freeLookComponent.enabled = true;
            }
    }
}
