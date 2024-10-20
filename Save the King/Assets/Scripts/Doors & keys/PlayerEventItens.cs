using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class PlayerEventItens : MonoBehaviour
{   
    // Start is called before the first frame update
    private bool isNearDoor = false;
    private bool isNearKey = false;
    public List<Key.KeyType> listOfKeys;
    private Door door;
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
     private void OnTriggerEnter(Collider other) {
            if(other.tag == "Door"){
                isNearDoor=true;
                door = other.gameObject.GetComponent<Door>();
                if(door.isClosed)door.textActivate();
            }else if(other.tag == "Key"){
                isNearKey=true;
                key = other.gameObject.GetComponent<Key>();
                key.textActivate();
            }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Door") {
            isNearDoor = false;        
            door = null;        
        }else if (other.tag == "Key") {
            isNearKey = false;        
            key = null;        
        }
    }
    public void AddKey(Key.KeyType keytype){
        listOfKeys.Add(keytype);
    }

    public void RemoveKey(Key.KeyType keytype){
        listOfKeys.Remove(keytype);
    }
    void Update(){
        if (controls.Gameplay.Interaction.triggered){
            if(isNearDoor){
                door.openDoor();
            }else if(isNearKey){
                key.grabKey(this.gameObject);
            }
        }
    }

    
    

    

    


}
