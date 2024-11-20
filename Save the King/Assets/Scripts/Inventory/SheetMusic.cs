using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetMusic : Item
{
    public override string GiveName()
    {
        return "SheetMusic";
    }

    public override string getNameWithSpaces()
    {
        return "Sheet Music";
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
        return Resources.Load<Sprite>("UI/Item Images/MapItem");
    }

    public override string GiveDiscresp()
    {
        return "A music sheet... I wonder what music it plays.";
    }
    public override Action UseFunc()
    {
        return () =>
        {
           GameObject.FindGameObjectWithTag("Organ").GetComponent<OrganInteractable>().AddPiece();
        };
    }
}
