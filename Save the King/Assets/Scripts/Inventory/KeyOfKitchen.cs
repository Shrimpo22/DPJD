using UnityEngine;
using System;
public class KeyOfKitchen : Item
{
    public override string GiveName()
    {
        return "KeyOfKitchen";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of Kitchen";
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
        return Resources.Load<Sprite>("UI/Item Images/KeyOfKitchen");
    }

    public override string GiveDiscresp()
    {
        return "A sturdy, rustic key that symbolizes the heart of the home, where all meals are made.";
    }
    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Kitchen Key");
            
        };
    }
}
