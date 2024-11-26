using UnityEngine;
using System;


public class RedItem : Item
{
    public override string GiveName()
    {
        return "RedItem";
    }

    public override string getNameWithSpaces()
    {
        return "Meat";
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
        return Resources.Load<Sprite>("UI/Item Images/RedItem");
    }

    public override string GiveDiscresp()
    {
        return "A prime cut of meat, full of flavor and ideal for hearty meals.";
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
                Debug.Log(GiveName() + " added to tentativa list.");
            }
            else
            {
                Debug.LogWarning("CozinharScript not found.");
            }
            
        };
    }
}

