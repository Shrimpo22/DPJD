using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamInteractable : Interactable
{
    protected Mouse mouse;

    private PlayerControls controls;
    public bool isLooking;
    protected GameObject inventory;
    protected GameObject player;
    public Camera myCamera;
    protected Camera mainCamera;
    
    public bool InvToOpen = true;
    public override void Start(){
        

        interactCanvas.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(false);
        
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Inventory");

        if(!InvToOpen){
            GameObject m = inventory.GetComponent<Inventory>().Mouse;
        
            if(m != null ){
                mouse = m.GetComponent<Mouse>();
                mouse.optionsDisplayed = false;
                mouse.item = null;
            }
        }

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        isLooking = false;
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        Debug.Log("[TimeScale] Pausing time in CamInteract");
        Time.timeScale = 0;
        controls = InputManager.inputActions;
        controls.Gameplay.Back.performed += ctx => HandleBack();
        
        if(InvToOpen == true){
            inv.openInventory();
        }else{
            inv.closeInventory();
        }

        if(!InvToOpen){
            Debug.Log("[Mouse] Mouse being showned by Cam");
            mouse.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        mainCamera.tag="Untagged";
        myCamera.tag="MainCamera";
        player.SetActive(false);
        mainCamera.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(true);
        inv.isZoomedIn=true;
        isLooking = true;
        interactCanvas.gameObject.SetActive(false);  
    }

    public virtual void ExitCam(){
        if(!InvToOpen){
            Debug.Log("[Mouse] Mouse being hidden by Cam");
            mouse.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }

        player.SetActive(true);
        mainCamera.tag = "MainCamera";
        myCamera.tag="Untagged";
        mainCamera.gameObject.SetActive(true);
        myCamera.gameObject.SetActive(false);
        isLooking = false;
        inventory.GetComponent<Inventory>().isZoomedIn = false;
        interactCanvas.gameObject.SetActive(true);  
        inventory.GetComponent<Inventory>().closeInventory();
        Debug.Log("[TimeScale] Resuming time in CamInteract");
        Time.timeScale = 1;
    }

    void HandleBack(){
        if(isLooking){
            ExitCam();
        }
    }
}
