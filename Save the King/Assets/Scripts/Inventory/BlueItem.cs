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
        return "Blue Item";
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
        return "Blue Item";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Blue Item");
            
        };
    }
}

