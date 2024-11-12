using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class CozinharScript : MonoBehaviour
{
    public TMP_Text textEvent;
    public bool isLooking = false;
    private bool isComplete = false;
    GameObject player;

    GameObject inventory;

    public Camera myCamera;
    private Camera mainCamera;
    public List<string> comidaCorreta = new List<string>();
    public List<string> comidaTentativa = new List<string>();
    private string finalAnswer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        comidaCorreta.Add("BlueItem");
        comidaCorreta.Add("RedItem");
        comidaCorreta.Add("BlueItem");
        comidaCorreta.Add("YellowItem");
        comidaCorreta.Add("GreenItem");

    }

    void Update()
    {
        if(comidaCorreta.Count == comidaTentativa.Count && !isComplete) {
            textEvent.text = "(ESC) Exit";
            isComplete=true;
            finalAnswer = string.Join(", ", comidaTentativa); 
            
            if (comidaCorreta.SequenceEqual(comidaTentativa)){
                Debug.Log("Acertou");
                inventory.GetComponent<Inventory>().AddItem("Potions",2);
                inventory.GetComponent<Inventory>().AddItem("KeyOfKitchen",1);
            }else{
                player.SetActive(true);
                GameObject targetObject = GameObject.FindGameObjectWithTag("Target");

                if (targetObject != null)
                {
                    // Obtém todos os componentes de MonoBehaviour (scripts) no GameObject
                    MonoBehaviour[] scripts = targetObject.GetComponents<MonoBehaviour>();

                    // Ativa cada script
                    foreach (MonoBehaviour script in scripts)
                    {
                        script.enabled = true;
                    }
                }
            }
        }    
        if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
            player.SetActive(true);
            textClear();
            mainCamera.tag = "MainCamera";
            myCamera.tag="Untagged";
            mainCamera.gameObject.SetActive(true);
            myCamera.gameObject.SetActive(false);
            isLooking = false;
            inventory.GetComponent<Inventory>().isLookingAtCook = false;
            inventory.GetComponent<Inventory>().InventoryMenu.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.E) && isLooking && !isComplete){
                inventory.GetComponent<Inventory>().OpenItCozinha();
                
        }
    
    }

    public void textActivate(){
      textEvent.text  = "(E) Interact";
      textEvent.gameObject.SetActive(true);  
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
        textEvent.text  = "(E) Add ingredient ; (ESC) Exit";
       } else{
        textEvent.text = "(ESC) Exit";}
        isLooking=true;

    }
    public void textClear(){
      textEvent.gameObject.SetActive(false);  
    }
    
}