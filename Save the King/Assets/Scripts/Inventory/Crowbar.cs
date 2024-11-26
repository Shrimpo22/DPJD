using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : Item
{
     public override string GiveName()
    {
        return "Crowbar";
    }

    public override string getNameWithSpaces()
    {
        return "Crowbar";
    }
    public override int MaxStacks()
    {
        return 1;
    }
    public override int Stats()
    {
        return 0;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/CrowbarItem");
    }

    public override string GiveDiscresp()
    {
        return "A rusted, grimy crowbar, its surface scarred and chipped";
    }
    public override Action UseFunc()
    {
        return () =>
        {
           GameObject.FindGameObjectWithTag("Wood").GetComponent<WoodInteractable>().AddPiece();
        };
    }
}