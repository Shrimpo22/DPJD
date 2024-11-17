using System.Collections;
using System.Collections.Generic;
using TMPro;
using Cinemachine;
using UnityEngine;
using Unity.VisualScripting;

public class CandleHolder : MonoBehaviour
{
    public TMP_Text textEvent;

    public int nrOfPiecesOn;
    private int totalPieces = 2;
    private bool isComplete = false;

    public bool isLooking = false;
    GameObject inventory;
    GameObject player;
    public Camera myCamera;
    private Camera mainCamera;
    
    public GameObject freelockCamara;
    private CinemachineFreeLook freeLookComponent;

    public GameObject drawer;
    public AudioSource audioSource;

    [SerializeField] public GameObject[] allPieces;
    
    void Start()
    {
       nrOfPiecesOn = 0 ;
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

    private void showFinalMap(){
        isComplete=true;
    }

    public void addPiece(){
       inventory.GetComponent<Inventory>().DropItemByName("Candle");
       allPieces[nrOfPiecesOn].SetActive(true);
       nrOfPiecesOn++;
        if (nrOfPiecesOn == 2) {
            gameObject.tag="Untagged";
            drawer.transform.position = drawer.transform.position + drawer.transform.forward * 0.3f;
            StartCoroutine(PlaySoundAndExit());
        }
    }
    private void OnTriggerExit(Collider other){
        textClear();
    }
    void Update()
    {
       if(nrOfPiecesOn == totalPieces && !isComplete) {
            textEvent.text = "(ESC) Exit";
            showFinalMap();
       }

       if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
           exitCam();
           

           
        }else if(Input.GetKeyDown(KeyCode.E) && isLooking && !isComplete){
                inventory.GetComponent<Inventory>().OpenIt();
                
        }
    }

     IEnumerator PlaySoundAndExit(){
        Debug.Log("Opening Drawer");

        if (audioSource.clip != null)
        {
            audioSource.Play();
            exitCam();
            yield return new WaitForSeconds(audioSource.clip.length + 1f);
            
            
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to AudioSource.");
        }

        Debug.Log("Finished");
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
