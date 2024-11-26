using UnityEngine;
using System;

public class BlueItem : Item
{
    public override string GiveName()
    {
        return "BlueItem";
    }

    public override string getNameWithSpaces()
    {
        return "Salt";
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
        return Resources.Load<Sprite>("UI/Item Images/BlueItem");
    }

    public override string GiveDiscresp()
    {
        return "Essential seasoning that enhances the taste of every dish.";
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

