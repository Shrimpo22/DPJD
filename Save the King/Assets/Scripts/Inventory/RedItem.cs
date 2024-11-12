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
        return "Red Item";
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
        return "Red Item";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            CozinharScript cozinharScript = GameObject.FindGameObjectWithTag("furnalha").GetComponent<CozinharScript>();
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

