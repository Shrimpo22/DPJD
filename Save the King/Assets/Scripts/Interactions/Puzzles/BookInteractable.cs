using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BookInteractable : CamInteractable
{
    public GameObject bookClose;
    public GameObject bookOpen;
    public GameObject textToRead;
    private EnemyRecipeInteractable targetInteraction;
    private int x;
    
    public override void Start()
    {
        InvToOpen = false;
        base.Start();
        GameObject targetObject = GameObject.FindGameObjectWithTag("NPCVIBES");
        if (targetObject != null)
        {
            targetInteraction = targetObject.GetComponent<EnemyRecipeInteractable>();
        }

        if (targetInteraction != null)
        {
            x = targetInteraction.textoInteracaoCount; // Acessa o valor inicial
        }
        else
        {
            Debug.LogError("Target object or Interaction script not found!");
        }
    }

    public override void ExitCam()
    {
        
        base.ExitCam();
        bookClose.SetActive(true);
        bookOpen.SetActive(false);
        textToRead.SetActive(false);
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        base.Interact(inv, playerItems);
        if (x >= 0)
        {
            player.SetActive(false);
            bookClose.SetActive(false);
            bookOpen.SetActive(true);
            myCamera.tag = "MainCamera";
            mainCamera.tag = "Untagged";
            mainCamera.gameObject.SetActive(false);
            myCamera.gameObject.SetActive(true);
            isLooking = true;
            textToRead.SetActive(true);
           
        }
        
        
        
    }
    void Update()
    {
        
        Inventory inven = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        if (targetInteraction != null)
        {
            x = targetInteraction.textoInteracaoCount;
        }
        if(isLooking){
            inven.closeInventory();
        }
    }
}
