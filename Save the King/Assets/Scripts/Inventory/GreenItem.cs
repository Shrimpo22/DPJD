using UnityEngine;
using System;


public class GreenItem : Item
{
    public override string GiveName()
    {
        return "GreenItem";
    }

    public override string getNameWithSpaces()
    {
        return "Potatoes";
    }
    public override int MaxStacks()
    {
        return 5;
    }

    public override int Stats()
    {
        return 0;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/GreenItem");
    }

    public override string GiveDiscresp()
    {
        return "Versatile and hearty, these potatoes are ready to be boiled, mashed, or fried.";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            FurnaceInteractable cozinharScript = GameObject.FindGameObjectWithTag("furnalha").GetComponent<FurnaceInteractable>();
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().DropItemByName(GiveName());
            if (cozinharScript != null)
            {
                cozinharScript.comidaTentativa.Add(GiveName());
                cozinharScript.AddItemToImage("GreenItem");
                Debug.Log(GiveName() + " added to tentativa list.");
            }
            else
            {
                Debug.LogWarning("CozinharScript not found.");
            }
            
        };
    }
}
