using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
public class BookOpening : MonoBehaviour
{
    public TMP_Text textEvent;

    public bool isLooking = false;
    
    GameObject player;
    public Camera myCamera;
    private Camera mainCamera;
    
    public GameObject freelockCamara;
    private CinemachineFreeLook freeLookComponent;

   
   
    void Start()
    {
       freeLookComponent = freelockCamara.GetComponent<CinemachineFreeLook>();
       player = GameObject.FindGameObjectWithTag("Player");
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
        Debug.Log("see me");
        mainCamera.tag="Untagged";
        myCamera.tag="MainCamera";
        player.SetActive(false);
        isLooking = true;
        mainCamera.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(true);
         textEvent.color = Color.black;
         textEvent.text = "(ESC) Exit";
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }
    void Update()
    {

       if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
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
            

           
        }
    }
}
