using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class PlayerEventItens : MonoBehaviour
{   
    public bool isNearDoor = false;
    public int rayCount = 2; 
    public float rayDistance = 3f; 
    public float spacing = 0.5f; 
    public List<Key.KeyType> listOfKeys;
    private IDoor door;
    private Key key;
    private PlayerControls controls;


    void Awake()
    {   
        controls = new PlayerControls();
    }
    void OnEnable(){
        controls.Gameplay.Enable();
    }

    void OnDisable(){
        controls.Gameplay.Disable();
    }
    public void AddKey(Key.KeyType keytype){
        listOfKeys.Add(keytype);
    }

    public void RemoveKey(Key.KeyType keytype){
        listOfKeys.Remove(keytype);
    }

     private void OnTriggerEnter(Collider other) {
        if(other.tag == "Door"){ 
            Debug.Log("Collided Door");
            door = other.gameObject.GetComponent<Door>();
            door = door != null ? door : other.gameObject.GetComponent<DoubleDoor>();
                if(door.IsClosed){
                    door.TextActivate();
                    isNearDoor = true;
                }}
    }
    void Update()  {
        if(isNearDoor && Input.GetKeyDown(KeyCode.E)){ door.OpenDoor(this.gameObject);
        }else{
        // Set the base position for raycasting
        Vector3 basePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        for (int x = -rayCount/2; x <= rayCount/2; x++)
        {
            for (int z = -rayCount; z <= rayCount; z++)
            {   
                Vector3 rayOrigin = basePosition + new Vector3(x * spacing, 1.5f, z * spacing);

                
                RaycastHit hit;
                Debug.DrawRay(rayOrigin, Vector3.down * rayDistance, Color.magenta);
                if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance))
                {
                    //Debug.Log("Hit: " + hit.collider.tag); // Log what was hit
                    HandleHit(hit.collider);
                    
                }
            }
        }
        }
    }
    private void HandleHit(Collider hit){

            Debug.Log("Hit " + hit.tag);

            if (hit.tag == "Key"){
                key = hit.gameObject.GetComponent<Key>();
                key.textActivate();
                if(Input.GetKeyDown(KeyCode.E)) key.grabKey(this.gameObject);
            
            }else if(hit.tag== "Chest") {
                hit.gameObject.GetComponent<OpenChest>().openChest();
            }else if(hit.tag == "Lock"  &&hit.gameObject.GetComponent<LockCombination>().isLooking==false){
               hit.gameObject.GetComponent<LockCombination>().textActivate();
               if(Input.GetKeyDown(KeyCode.E)) hit.gameObject.GetComponent<LockCombination>().seeLock();
            }else if(hit.tag == "MirrorPiece"){
                MirrorPiece mirrorPiece = hit.gameObject.GetComponent<MirrorPiece>();
                mirrorPiece.textActivate();
                if(Input.GetKeyDown(KeyCode.E)) mirrorPiece.grabMirrorPiece();
            }else if(hit.tag == "Candelabra" && hit.gameObject.GetComponent<CandelabraCam>().isLooking == false){
                hit.gameObject.GetComponent<CandelabraCam>().textActivate();
                if(Input.GetKeyDown(KeyCode.E))hit.gameObject.GetComponent<CandelabraCam>().seeObject();
            }else if(hit.tag == "MirrorTable" && hit.gameObject.GetComponent<MirrorTableCam>().isLooking == false){
                hit.gameObject.GetComponent<MirrorTableCam>().textActivate();
                if(Input.GetKeyDown(KeyCode.E))hit.gameObject.GetComponent<MirrorTableCam>().seeObject();
            }

    }
}

    
    

    

    


