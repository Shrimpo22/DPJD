using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Item
{
    // Start is called before the first frame update
    public override string GiveName()
    {
        return "Torch";
    }

    public override string getNameWithSpaces()
    {
        return "Torch";
    }
    public override int MaxStacks()
    {
        return 2;
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
        return "A torch... It is not supposed to be here...";
    }
    public override Action UseFunc()
    {
        return () =>
        {
           GameObject.FindGameObjectWithTag("Statue").GetComponent<Statues>().addPiece(GiveName());
        };
    }
}
