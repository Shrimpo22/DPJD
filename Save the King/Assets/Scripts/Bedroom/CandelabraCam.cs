using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CandelabraCam : MonoBehaviour
{

    public TMP_Text textEvent;

    public bool isLooking = false;
    GameObject inventory;

    GameObject player;
    public Camera myCamera;
    private Camera mainCamera;
    public bool active = true;    
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
        Debug.Log("Exited");
      textEvent.gameObject.SetActive(false);  
    }
    public void seeObject() {
        mainCamera.tag="Untagged";
        myCamera.tag="MainCamera";
        player.SetActive(false);
        isLooking = true;
        mainCamera.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(true);
        textEvent.enabled = false;
        isLooking=true;

    }
    private void OnTriggerExit(Collider other){
        textClear();
    }

    public void exitCam(){
        player.SetActive(true);
        textEvent.enabled = false;
        mainCamera.tag = "MainCamera";
        myCamera.tag="Untagged";
        mainCamera.gameObject.SetActive(true);
        myCamera.gameObject.SetActive(false);
        isLooking = false;
        inventory.GetComponent<Inventory>().isZoomedIn = false;
    }

    public void exitCamAndDisable(){
        exitCam();
        active = false;
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
           exitCam(); 
        }
    }
    
}
