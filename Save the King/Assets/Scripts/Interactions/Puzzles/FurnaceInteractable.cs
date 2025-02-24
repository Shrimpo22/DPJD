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
    private CamInteractable aaa;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    private List<GameObject> imageSlots;
    private Dictionary<string, Sprite> itemSprites;
    
    public override void Start()
    {
        InvToOpen = true;
        base.Start();
        
        i = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        comidaCorreta.Add("BlueItem");
        comidaCorreta.Add("RedItem");
        comidaCorreta.Add("BlueItem");
        comidaCorreta.Add("YellowItem");
        comidaCorreta.Add("GreenItem");

        imageSlots = new List<GameObject> { image1, image2, image3, image4, image5 };

        itemSprites = new Dictionary<string, Sprite>
        {
            { "BlueItem", Resources.Load<Sprite>("UI/Item Images/BlueItem") },
            { "RedItem", Resources.Load<Sprite>("UI/Item Images/RedItem") },
            { "YellowItem", Resources.Load<Sprite>("UI/Item Images/YellowItem") },
            { "GreenItem", Resources.Load<Sprite>("UI/Item Images/GreenItem") }
        };

        ClearAllImages();

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
            }else{
                conf.SetActive(false);
            }
            if(confirmd == false){
                foreach (var image in imageSlots)
                {
                    image.SetActive(true);
                }  
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

    public override void ExitCam()
    {
        base.ExitCam();
        foreach (var image in imageSlots)
        {
            image.SetActive(false);
        }
        conf.SetActive(false);
        refazer.SetActive(false);
        aguaFerver.SetActive(false);
        inventory.GetComponent<Inventory>().isLookingAtCook = false;
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)  {
        
        base.Interact(inv, playerItems);
        inv.isLookingAtCook=true;
        aguaFerver.SetActive(true);
        
    }
    
    public void confirmado(){
        confirmd = true;
        interacao = GameObject.FindGameObjectWithTag("NPCVIBES").GetComponent<EnemyRecipeInteractable>();
        interacao.textoInteracaoCount +=1;
        interacao.InvToOpen = true;
        interacao.icon.SetActive(false);
        foreach (var image in imageSlots)
        {
            image.SetActive(false);
        }
    }
    public void AddItemToImage(string itemName)
    {
        for (int i = 0; i < imageSlots.Count; i++)
        {
            if (imageSlots[i].GetComponent<UnityEngine.UI.Image>().sprite == null)
            {
                if (itemSprites.TryGetValue(itemName, out Sprite sprite))
                {
                    imageSlots[i].GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
                break;
            }
        }
    }
    public void undo(){
        for (int i = comidaTentativa.Count - 1; i >= 0; i--)
        {
            inventory.GetComponent<Inventory>().AddItem(comidaTentativa[i], 1);
        }
        comidaTentativa = new List<string>();
        ClearAllImages();
    }

    private void ClearAllImages()
    {
        foreach (var image in imageSlots)
        {
            image.GetComponent<UnityEngine.UI.Image>().sprite = null;
        }
    }

}
 