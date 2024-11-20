using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamInteractable : Interactable
{
    private PlayerControls controls;
    public bool isLooking;
    protected GameObject inventory;
    protected GameObject player;
    public Camera myCamera;
    protected Camera mainCamera;

    public override void Start(){
        interactCanvas.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(false);
        
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        isLooking = false;
    }
    
    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        controls = InputManager.inputActions;
        controls.Gameplay.Back.performed += ctx => HandleBack();
        controls.Gameplay.Inventory.performed += ctx => HandleInv();
        controls.Gameplay.Interaction.performed += ctx => HandleInv();

        mainCamera.tag="Untagged";
        myCamera.tag="MainCamera";
        player.SetActive(false);
        mainCamera.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(true);
        isLooking=true;
        interactCanvas.gameObject.SetActive(false);  

    }

    public virtual void ExitCam(){
        player.SetActive(true);
        mainCamera.tag = "MainCamera";
        myCamera.tag="Untagged";
        mainCamera.gameObject.SetActive(true);
        myCamera.gameObject.SetActive(false);
        isLooking = false;
        inventory.GetComponent<Inventory>().isZoomedIn = false;
        interactCanvas.gameObject.SetActive(true);  
        inventory.GetComponent<Inventory>().closeInventory();
        Time.timeScale = 1;
    }

    void HandleBack(){
        if(isLooking){
            ExitCam();
        }
    }

    public virtual void HandleInv(){
        if(isLooking){
            inventory.GetComponent<Inventory>().OpenIt();
        }
    }
}
