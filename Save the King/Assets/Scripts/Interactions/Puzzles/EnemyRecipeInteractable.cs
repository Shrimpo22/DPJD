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

    public GameObject texto;
    public TMP_Text textInteraction;
    public int textoInteracaoCount = 1;
    private int count = 0;

    public override void Start()
    {
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
                inventory.GetComponent<Inventory>().AddItem("KeyOfKitchen", 1);
                targetObject.tag="Untagged";
                wrong = true;
            }

            Onetime += 1;

        }

        if (fighting == true && mainCamera.gameObject.activeSelf)
        {
            AiAgent aiAgent = targetObject.GetComponent<AiAgent>();
            if (aiAgent != null && aiAgent.currentHealth == 0 && OnetimeV2 == 0)
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
        textoInteracaoCount += 1;
        texto.SetActive(false);

        if (usedItem && !wrong && targetObject != null)
        {
            MonoBehaviour[] scripts = targetObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true;
                fighting = true;
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

        }
    }

    public void useItem()
    {
        usedItem = true;
    }
}
