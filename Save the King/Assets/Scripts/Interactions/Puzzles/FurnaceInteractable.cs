using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


public class FurnaceInteractable : CamInteractable
{
    private bool isComplete = false;
    private bool confirmd = false;
    public bool isright = false;
    public GameObject aguaFerver;
    public GameObject conf;
    public GameObject refazer;
    public List<string> comidaCorreta = new List<string>();
    public List<string> comidaTentativa = new List<string>();
    private Inventory i;
    private EnemyRecipeInteractable interacao;
    public override void Start()
    {
        base.Start();
        i = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        comidaCorreta.Add("BlueItem");
        comidaCorreta.Add("RedItem");
        comidaCorreta.Add("BlueItem");
        comidaCorreta.Add("YellowItem");
        comidaCorreta.Add("GreenItem");

    }

    void Update()
    {


        if (i.isLookingAtCook == true)
        {
            if (comidaTentativa.Count > 0 && !isComplete)
            {
                refazer.SetActive(true);
            }
            else if (comidaTentativa.Count == 0 && !isComplete)
            {
                refazer.SetActive(false);
            }
            if (comidaTentativa.Count == 5 && !isComplete)
            {
                conf.SetActive(true);
            }
        }
        

        if(comidaCorreta.Count == comidaTentativa.Count && !isComplete && confirmd) {
            
            isComplete=true;
            conf.SetActive(false);
            refazer.SetActive(false);
            if (comidaCorreta.SequenceEqual(comidaTentativa)){
                isright = true;
            }

            inventory.GetComponent<Inventory>().AddItem("Plate",1);
            
        }    
    
    }

    public override void HandleInv(){
        if(isLooking){
            aguaFerver.SetActive(true);
            inventory.GetComponent<Inventory>().OpenIt();
        }
    }

    public override void ExitCam()
    {
        base.ExitCam();
        conf.SetActive(false);
        refazer.SetActive(false);
        aguaFerver.SetActive(false);
        inventory.GetComponent<Inventory>().isLookingAtCook = false;
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)  {
        base.Interact(inv, playerItems);
        inv.isLookingAtCook=true;
        inv.OpenIt();
        aguaFerver.SetActive(true);
    }
    
    public void confirmado(){
        confirmd = true;
        interacao = GameObject.FindGameObjectWithTag("NPCVIBES").GetComponent<EnemyRecipeInteractable>();
        interacao.textoInteracaoCount +=1;
    }
    
    public void undo(){
        for (int i = comidaTentativa.Count - 1; i >= 0; i--)
        {
            inventory.GetComponent<Inventory>().AddItem(comidaTentativa[i], 1);
        }
        comidaTentativa = new List<string>();
    }

}
 