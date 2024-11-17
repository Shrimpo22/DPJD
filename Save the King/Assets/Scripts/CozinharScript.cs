using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Cinemachine;
public class CozinharScript : MonoBehaviour
{
    public TMP_Text textEvent;
    public bool isLooking = false;
    private bool isComplete = false;
    private bool confirmd = false;
    public bool isright = false;
    GameObject player;

    GameObject inventory;

    public GameObject freelockCamara;
    public GameObject conf;
    public GameObject refazer;
    public Camera myCamera;
    private Camera mainCamera;
    public List<string> comidaCorreta = new List<string>();
    public List<string> comidaTentativa = new List<string>();
    private CinemachineFreeLook freeLookComponent;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        freeLookComponent = freelockCamara.GetComponent<CinemachineFreeLook>();
        comidaCorreta.Add("BlueItem");
        comidaCorreta.Add("RedItem");
        comidaCorreta.Add("BlueItem");
        comidaCorreta.Add("YellowItem");
        comidaCorreta.Add("GreenItem");

    }

    void Update()
    {
        if(comidaTentativa.Count > 0 && !isComplete){
            refazer.SetActive(true);
        }
        else if(comidaTentativa.Count == 0 && !isComplete){
            refazer.SetActive(false);
        }

        if(comidaTentativa.Count == 5 && !isComplete)
        {
            conf.SetActive(true);
        }

        if(comidaCorreta.Count == comidaTentativa.Count && !isComplete && confirmd) {
            
            isComplete=true;
            textEvent.text = "(ESC) Exit";
            conf.SetActive(false);
            refazer.SetActive(false);
            if (comidaCorreta.SequenceEqual(comidaTentativa)){
                isright = true;
            }

            inventory.GetComponent<Inventory>().AddItem("Plate",1);
            
        }    
        if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
            player.SetActive(true);
            Time.timeScale = 1;
            textClear();
            mainCamera.tag = "MainCamera";
            myCamera.tag="CozinhaCamara";
            mainCamera.gameObject.SetActive(true);
            myCamera.gameObject.SetActive(false);
            if (freeLookComponent != null)
            {
                freeLookComponent.enabled = true;
            }
            conf.SetActive(false);
            refazer.SetActive(false);
            isLooking = false;
            inventory.GetComponent<Inventory>().isLookingAtCook = false;
            inventory.GetComponent<Inventory>().InventoryMenu.SetActive(false);
            
        }
        else if(Input.GetKeyDown(KeyCode.E) && isLooking && !isComplete){
                inventory.GetComponent<Inventory>().OpenItCozinha();
                
        }
    
    }

    public void textActivate(){
      textEvent.text = "(ESC) Exit";
      textEvent.gameObject.SetActive(true);
      textEvent.color  = Color.white;  
    }

    public void seeObject() {
        mainCamera.tag="CozinhaCamara";
        myCamera.tag="MainCamera";
        player.SetActive(false);
        isLooking = true;
        mainCamera.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(true);
        textEvent.color = Color.black;
        if(!isComplete){
            textEvent.text  = "(ESC) Exit";
        }else{
        textEvent.text = "(ESC) Exit";}
        

    }
    public void textClear(){
      textEvent.gameObject.SetActive(false);  
    }

    public void confirmado(){
        confirmd = true;
    }
    
    public void undo(){
        for (int i = comidaTentativa.Count - 1; i >= 0; i--)
        {
            inventory.GetComponent<Inventory>().AddItem(comidaTentativa[i], 1);
        }
        comidaTentativa = new List<string>();
    }


    private void OnTriggerExit(Collider other){
        textClear();
    }
    private void OnTriggerEnter(Collider other){
            textActivate();
    }
}