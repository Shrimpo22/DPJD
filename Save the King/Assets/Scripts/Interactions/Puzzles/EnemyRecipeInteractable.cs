using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


public class EnemyRecipeInteractable : CamInteractable
{
    public bool isTalkingToNpc = false;
    private int Onetime = 0;
    private int OnetimeV2 = 0;
    public bool usedItem = false;
    public bool fighting = false;
    GameObject targetObject;
    private bool wrong = false;
    public GameObject Inimigo;
    public GameObject texto;
    public TMP_Text textInteraction;
    public int textoInteracaoCount = 1;
    public GameObject icon;
    public GameObject interact;

    public GameObject healthBar;
    
    public override void Start()
    {
        healthBar.SetActive(false);
        InvToOpen = false;
        base.Start();
        targetObject = GameObject.FindGameObjectWithTag("NPCVIBES");
    }

    void Update()
    {
        
        if (isTalkingToNpc && Onetime == 0 && usedItem)
        {
            FurnaceInteractable cozinharScript = GameObject.FindGameObjectWithTag("furnalha").GetComponent<FurnaceInteractable>();
            if (cozinharScript.isright == true)
            {
                inventory.GetComponent<Inventory>().AddItem("Potions", 2);
                inventory.GetComponent<Inventory>().AddItem("KeyOfFeastRoom", 1);
                
                targetObject.tag="Untagged";
                wrong = true;
                
            }

            Onetime += 1;

        }                
    }

    public override void ExitCam()
    {
        base.ExitCam();
        isTalkingToNpc = false;
    
        texto.SetActive(false);

        if (usedItem && !wrong && Inimigo != null)
        {
            MonoBehaviour[] scripts = Inimigo.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if(script is AiAgent aiAgent) aiAgent.isNpc = false;
                script.enabled = true;
                fighting = true;
                
                
            }
            healthBar.SetActive(true);
            targetObject.tag = "Target";
            
            
        }
        
        
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {

        base.Interact(inv, playerItems);
    
        isTalkingToNpc = true;
        if (textoInteracaoCount == 1)
        {
            textInteraction.text = "I want you to make me a dish, go to the book on top of the table do the 3rd recipe, the ingredients are on the top of the tables , if you do it successfully you will receive a reward and the key, if you dont good luck ";
            texto.gameObject.SetActive(true);
            icon.SetActive(false);

        }
       
    }

    public void useItem()
    {
        usedItem = true;
        gameObject.layer=0;
        icon.SetActive(false);
    }
}
