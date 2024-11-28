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
    private int count = 0;
    public GameObject icon;
    public GameObject interact;
    
    public override void Start()
    {
        base.Start();
        targetObject = GameObject.FindGameObjectWithTag("Target");
    }

    void Update()
    {
        if (isTalkingToNpc && Onetime == 0 && usedItem)
        {
            FurnaceInteractable cozinharScript = GameObject.FindGameObjectWithTag("furnalha").GetComponent<FurnaceInteractable>();
            if (cozinharScript.isright == true)
            {
                inventory.GetComponent<Inventory>().AddItem("Potions", 2);
                inventory.GetComponent<Inventory>().AddItem("KeyOfKitchen", 1);
                targetObject.tag="Untagged";
                wrong = true;
            }

            Onetime += 1;

        }
        if (fighting && mainCamera.gameObject.activeSelf)
        {
            AiAgent aiAgent = Inimigo.GetComponent<AiAgent>();
            // Verifica se o componente AiAgent está ativo antes de entrar no if
            if (aiAgent != null && aiAgent.enabled && aiAgent.currentHealth <= 0 && OnetimeV2 == 0 && aiAgent.maxHealth == 60)
            {
                inventory.GetComponent<Inventory>().AddItem("KeyOfKitchen", 1);
                OnetimeV2 += 1;
            }
        }
        
    }

    public override void ExitCam()
    {
        base.ExitCam();
        isTalkingToNpc = false;
        inventory.GetComponent<Inventory>().isLookingAtCook = false;
        texto.SetActive(false);

        if (usedItem && !wrong && Inimigo != null)
        {
            MonoBehaviour[] scripts = Inimigo.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if(script is AiAgent aiAgent) aiAgent.isNpc = false;
                script.enabled = true;
                fighting = true;
                interact.SetActive(false);
                
            }
            
        }
        
        targetObject.tag = "Target";

    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        base.Interact(inv, playerItems);
        inv.isLookingAtCook = true;
        isTalkingToNpc = true;
        if (textoInteracaoCount == 1)
        {
            textInteraction.text = "I want you to make me a dish, go to the book on top of the table do the 3rd recipe, the ingredients are on the vases , if you do it successfully you will receive a reward and the key, if you dont good luck ";
            texto.gameObject.SetActive(true);
            icon.SetActive(false);

        }
    }

    public void useItem()
    {
        usedItem = true;
    }
}
