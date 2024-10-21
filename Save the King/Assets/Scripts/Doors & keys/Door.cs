using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour{
    [SerializeField] private bool isLocked;
    [SerializeField] public TMP_Text textDoor;
    public bool isClosed=true;
     private float rotationSpeed = 90f;  
    private float currentRotation = 0f; 
    private float targetRotation = 0f; 

    private AudioSource audioSource;
    public AudioClip lockedSound;
    public AudioClip unlockedOpenSound;
     [SerializeField] Key.KeyType keyType;
    
    void Start(){ audioSource = GetComponent<AudioSource>();
        if (audioSource == null){
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private Key.KeyType getKeyType(){
        return keyType;
    }
    
    public void textActivate(){
      textDoor.text  = "(E) Open Door";
      textDoor.gameObject.SetActive(true);  
    }

    public void textClear(){
      textDoor.gameObject.SetActive(false);  
    }
    public void openDoor() {
        if ( !isLocked && isClosed){
            targetRotation=90f;
            textClear();
        }else if(isLocked){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            if(player.GetComponent<PlayerEventItens>().listOfKeys.Count>0 && player.GetComponent<PlayerEventItens>().listOfKeys.Contains(getKeyType())){
                audioSource.clip = unlockedOpenSound;
                audioSource.Play();
                isLocked = false;
                player.GetComponent<PlayerEventItens>().RemoveKey(getKeyType());
                inventory.GetComponent<Inventory>().DropItemByName(getKeyType().ToString());
                openDoor();
                
            }else{
            audioSource.clip = lockedSound;
            audioSource.Play();
            textDoor.text  = "Locked";
            }
        }
    }


    public void OnTriggerExit(Collider other){
        textClear();
    }

    public void Update(){
       
        if (!isLocked && isClosed && currentRotation < targetRotation) {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;  
            transform.Rotate(new Vector3(0, -rotationThisFrame, 0), Space.Self);  
            currentRotation += rotationThisFrame; 

            if (currentRotation >= targetRotation) {
                targetRotation = 0f;
                currentRotation = 0f;
                isClosed = false;  
            }
        }
    }



    

    
}
