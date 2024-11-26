using UnityEngine;
using System;

public class YellowItem : Item
{
    public override string GiveName()
    {
        return "YellowItem";
    }

    public override string getNameWithSpaces()
    {
        return "Eggs";
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
        return Resources.Load<Sprite>("UI/Item Images/YellowItem");
    }

    public override string GiveDiscresp()
    {
        return "Freshly laid eggs, perfect for any recipe requiring a touch of nature's simplicity.";
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
