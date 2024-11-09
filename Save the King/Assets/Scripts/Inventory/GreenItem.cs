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
        return "Green Item";
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
        return "Green Item";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Green Item");
            
        };
    }
}
