using UnityEngine;
using System;
public class KeyOfGeneralRoom2 : Item
{
     public override string GiveName()
    {
        return "KeyOfGeneralRoom2";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of General Room2";
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
        return Resources.Load<Sprite>("UI/Item Images/KeyOfGeneralRoom2");
    }

    public override string GiveDiscresp()
    {
        return "General Room Key";
    }
    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("General Room Key");
            
        };
    }
}
