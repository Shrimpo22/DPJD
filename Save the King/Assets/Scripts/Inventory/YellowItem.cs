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
        return "Yellow Item";
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
        return "Yellow Item";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Yellow Item");
            
        };
    }
}
