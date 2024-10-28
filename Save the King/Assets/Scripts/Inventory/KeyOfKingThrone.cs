using UnityEngine;
using System;
public class KeyOfKingThrone  : Item
{
    public override string GiveName()
    {
        return "KeyOfKingThrone";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of King Throne";
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
        return Resources.Load<Sprite>("UI/Item Images/KeyOfKingThrone");
    }

    public override string GiveDiscresp()
    {
        return "An elegantly crafted key adorned with regal motifs, embodying authority and significance.";
    }
    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Throne Key");
            
        };
    }
}