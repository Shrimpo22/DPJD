using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaintingCam : MonoBehaviour
{
    public TMP_Text textEvent;

    public bool isLooking = false;
    GameObject inventory;

    GameObject player;
    public Camera myCamera;
    private Camera mainCamera;
    public bool active = true;

    public GameObject Key;
    Rigidbody rb;    
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
       inventory = GameObject.FindGameObjectWithTag("Inventory");
       mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
       rb = GetComponent<Rigidbody>();
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
        inventory.GetComponent<Inventory>().isLookingAtMap = false;
    }

    public void exitCamAndDisable(){
        exitCam();
        active = false;
    }

    public void OnMouseDown(){
        if(active && isLooking){
            rb.isKinematic = false;
            rb.AddExplosionForce(500f, transform.position, 2f, 0f);
            Vector3 forwardForce = Vector3.Cross(transform.forward, transform.right) * 15f;
        
            // Apply the force in the forward direction of the rigid body~
            Vector3 bottomRightCorner = transform.position + transform.right * 0.5f - transform.up * 0.5f; 
            rb.AddForceAtPosition(forwardForce, bottomRightCorner, ForceMode.Force);
            textEvent.enabled = false;
            active = false;
            Key.SetActive(true);
        }
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
           exitCam(); 
        }
    }
    
}

