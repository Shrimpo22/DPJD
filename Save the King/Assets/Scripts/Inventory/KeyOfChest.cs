using UnityEngine;
using System;
public class KeyOfChest : Item
{
    public override string GiveName()
    {
        return "KeyOfChest";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of Chest";
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
        return Resources.Load<Sprite>("UI/Item Images/KeyOfChest");
    }

    public override string GiveDiscresp()
    {
        return "A small and mysterious key, ready to unlock hidden treasures.";
    }
    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Chest Key");
            
        };
    }
}
