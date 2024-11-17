using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;
public class Organ : MonoBehaviour
{
    public TMP_Text textEvent;
    private int totalPieces = 1;
    private bool isComplete = false;

    public bool isLooking = false;
    GameObject inventory;
    public bool shownChalice = false;
    GameObject player;
    public Camera myCamera;
    private Camera mainCamera;

    public GameObject paperMusic;
    public GameObject freelockCamara;
    private CinemachineFreeLook freeLookComponent;

    public GameObject BookOpen;
    public GameObject BookClosed;

    public AudioSource audioSource;
    
    void Start()
    {
       paperMusic.SetActive(false);
       freeLookComponent = freelockCamara.GetComponent<CinemachineFreeLook>();
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


    public void addPiece(){
       inventory.GetComponent<Inventory>().DropItemByName("SheetMusic");
       paperMusic.SetActive(true);
       isComplete = true;
        audioSource.Play();
        BookOpen.SetActive(true);
        BookClosed.SetActive(false);
        gameObject.tag="Untagged";
        exitCam();
    }


    private void OnTriggerExit(Collider other){
        textClear();
    }
    void Update()
    {
       if(isComplete ) {
            textEvent.text = "(ESC) Exit";
            if(!shownChalice){
                shownChalice = true;
                //showChalice();
            }
       }

       if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
            exitCam();
           
           
        }else if(Input.GetKeyDown(KeyCode.E) && isLooking && !isComplete){
                inventory.GetComponent<Inventory>().OpenIt();
                
        }
    }

    public void exitCam(){
        player.SetActive(true);
            
            Time.timeScale = 1;
             if (freeLookComponent != null)
            {
                freeLookComponent.enabled = true;
            }
            textClear();
            mainCamera.tag = "MainCamera";
            myCamera.tag="Untagged";
            mainCamera.gameObject.SetActive(true);
            myCamera.gameObject.SetActive(false);
            isLooking = false;
            inventory.GetComponent<Inventory>().isLookingAtMap = false;
            inventory.GetComponent<Inventory>().InventoryMenu.SetActive(false);
    }
}
