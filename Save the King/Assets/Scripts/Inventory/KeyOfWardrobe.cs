using UnityEngine;
using System;

public class KeyOfWardrobe : Item
{
    public override string GiveName()
    {
        return "KeyOfWardrobe";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of Wardrobe";
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
        return "A small, intricately carved wooden key for a medieval wardrobe.";
    }
    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Wardrobe Key");
            
        };
    }
}
