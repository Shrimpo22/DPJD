using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class BestSword : Item
{
    public override string GiveName()
    {
        return "BestSword";
    }

    public override string getNameWithSpaces()
    {
        return "Sharp Sword";
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
        return Resources.Load<Sprite>("UI/Item Images/BestSwordItem");
    }

    public override string GiveDiscresp()
    {
        return "Light ATK -> 50 DMG ; Heavy ATK -> 65 DMG";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEventItens>().InstantiateSword(GiveName());
        };
    }
}
