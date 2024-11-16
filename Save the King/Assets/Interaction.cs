using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Cinemachine;
public class Interaction : MonoBehaviour
{
    public TMP_Text textEvent;
    public bool isTalkingToNpc = false;
    private int Onetime = 0;
    private int OnetimeV2 = 0;
    public bool usedItem = false;
    public Camera myCamera;
    public Camera mainCamera;
    public bool fighting = false;
    GameObject inventory;
    GameObject player;
    GameObject targetObject;
    public GameObject freelockCamara;
    private CinemachineFreeLook freeLookComponent;
    private bool wrong = false;

    public GameObject texto;
    public TMP_Text textInteraction;
    private int textoInteracaoCount=1;
    private int count = 0;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        player = GameObject.FindGameObjectWithTag("Player");
        targetObject = GameObject.FindGameObjectWithTag("Target");
        freeLookComponent = freelockCamara.GetComponent<CinemachineFreeLook>();
    }
    void Update()
    {
        if (isTalkingToNpc && Onetime ==0 && usedItem){
            CozinharScript cozinharScript = GameObject.FindGameObjectWithTag("furnalha").GetComponent<CozinharScript>();
            if (cozinharScript.isright == true){
                inventory.GetComponent<Inventory>().AddItem("Potions",2);
                inventory.GetComponent<Inventory>().AddItem("KeyOfKitchen",1);

                wrong = true;
            }

            Onetime +=1;
            
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isTalkingToNpc){
            player.SetActive(true);
            Time.timeScale = 1;
            textClear();
            mainCamera.tag = "MainCamera";
            myCamera.tag="Untagged";
            mainCamera.gameObject.SetActive(true);
            myCamera.gameObject.SetActive(false);
            isTalkingToNpc = false;
            inventory.GetComponent<Inventory>().isLookingAtCook = false;
            inventory.GetComponent<Inventory>().InventoryMenu.SetActive(false);
            textoInteracaoCount+=1;
            if (freeLookComponent != null)
            {
                freeLookComponent.enabled = true;
            }
            if(usedItem){
               if (!wrong){
                    if (targetObject != null)
                    {
                        MonoBehaviour[] scripts = targetObject.GetComponents<MonoBehaviour>();

                        foreach (MonoBehaviour script in scripts)
                        {
                            script.enabled = true;
                            fighting = true;
                        }
                        textClear();
                    }
                } 
            }
            
            
        
        }
        if(textoInteracaoCount != 1){
            Debug.Log("Entrei");
            if(Input.GetKeyDown(KeyCode.E) && isTalkingToNpc){
                inventory.GetComponent<Inventory>().OpenItCozinha();
                            
            }
        }
        



        if (fighting == true && mainCamera.gameObject.activeSelf){
                AiAgent aiAgent = targetObject.GetComponent<AiAgent>();
                if (aiAgent != null && aiAgent.currentHealth == 0 && OnetimeV2 == 0)
                {
                        inventory.GetComponent<Inventory>().AddItem("KeyOfKitchen", 1);
                        OnetimeV2+=1;
                }

        }
    }

    public void seeObject() {
        mainCamera.tag="CozinhaCamara";
        myCamera.tag="MainCamera";
        player.SetActive(false);
        isTalkingToNpc = true;
        mainCamera.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(true);
        textEvent.color = Color.white;
    }

    public void textActivate(){
      textEvent.text  = "(ESQ) EXIT";
      textEvent.gameObject.SetActive(true);  
    }
    public void InteracaText(){
        if(textoInteracaoCount == 1){
            textInteraction.text = "I want you to make me a plate (1) , if you do it successfully you will recive a reward and the key, if you dont good luck ";
            texto.gameObject.SetActive(true);
            
        }
        
    }
    
    public void useItem(){
        usedItem = true;
    }
    public void textClear(){
      textEvent.gameObject.SetActive(false);  
      texto.gameObject.SetActive(false);
    }
}
