using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;
using TMPro;
public class Book : MonoBehaviour
{
    public TMP_Text bookText;
    public GameObject bookClose;
    public GameObject bookOpen;
    public GameObject textToRead;
    private bool isLooking = false;
    public Camera myCamera;
    public Camera mainCamera;
    public GameObject freelockCamara;
    private CinemachineFreeLook freeLookComponent;
    GameObject player;
    private Interaction targetInteraction;
    private int x;
    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag("Target");
        if (targetObject != null)
        {
            targetInteraction = targetObject.GetComponent<Interaction>();
        }
        freeLookComponent = freelockCamara.GetComponent<CinemachineFreeLook>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (targetInteraction != null)
        {
            x = targetInteraction.textoInteracaoCount; // Acessa o valor inicial
        }
        else
        {
            Debug.LogError("Target object or Interaction script not found!");
        }
    }
    void Update()
    {
        if (targetInteraction != null)
        {
            x = targetInteraction.textoInteracaoCount;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
            bookClose.SetActive(true);
            bookOpen.SetActive(false);
            player.SetActive(true);
            mainCamera.tag = "MainCamera";
            myCamera.tag="Untagged";
            mainCamera.gameObject.SetActive(true);
            myCamera.gameObject.SetActive(false);
            if (freeLookComponent != null)
            {
                freeLookComponent.enabled = true;
            }
            isLooking = false;
            textToRead.SetActive(false);
        }
    }
    
    public void openbook(){
        if(x>1){
            player.SetActive(false);
            bookClose.SetActive(false);
            bookOpen.SetActive(true);
            myCamera.tag = "MainCamera";
            mainCamera.tag="Untagged";
            mainCamera.gameObject.SetActive(false);
            myCamera.gameObject.SetActive(true);       
            isLooking = true;
            textToRead.SetActive(true); 
        }
        

    }
    public void textActivate(){
      bookText.text  = "(E) Read Book";
      bookText.color = Color.white;
      bookText.gameObject.SetActive(true);  
    }
    public void textClear(){
      bookText.gameObject.SetActive(false);  
    }
    private void OnTriggerEnter(Collider other){
            textActivate();
        }
    private void OnTriggerExit(Collider other){
        textClear();
    }


    

}
