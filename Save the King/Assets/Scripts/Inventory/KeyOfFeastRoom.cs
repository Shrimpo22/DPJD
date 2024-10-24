using UnityEngine;
using System;
public class KeyOfFeastRoom : Item
{
    public override string GiveName()
    {
        return "KeyOfFeastRoom";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of Feast Room";
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
        return Resources.Load<Sprite>("UI/Item Images/KeyOfFeastRoom");
    }

    public override string GiveDiscresp()
    {
        return "Feast Key";
    }
    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Feast Key");
            
        };
    }
}
