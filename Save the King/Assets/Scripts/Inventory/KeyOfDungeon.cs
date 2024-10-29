using UnityEngine;
using System;

public class KeyOfDungeon : Item
{
    public override string GiveName()
    {
        return "KeyOfDungeon";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of Dungeon";
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
        return "A key that unlocks the door for freedom.";
    }
    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Dungeon Key");
            
        };
    }
}
