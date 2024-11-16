using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PaintingInteractable : CamInteractable
{
    public bool active;
    public GameObject Key;
    Rigidbody rb;

    public override void Start(){
        active = false;
        interactCanvas.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(false);
        
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        isLooking = false;
    }

    public void OnMouseDown(){
        if(active && isLooking){
            rb.isKinematic = false;
            rb.AddExplosionForce(500f, transform.position, 2f, 0f);
            Vector3 forwardForce = Vector3.Cross(transform.forward, transform.right) * 15f;
        
            // Apply the force in the forward direction of the rigid body~
            Vector3 bottomRightCorner = transform.position + transform.right * 0.5f - transform.up * 0.5f; 
            rb.AddForceAtPosition(forwardForce, bottomRightCorner, ForceMode.Force);
            interactCanvas.enabled = false;
            active = false;
            Key.SetActive(true);
        }
    }
    
}
