using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class BlackSword : Item
{
    public override string GiveName()
    {
        return "BlackSword";
    }

    public override string getNameWithSpaces()
    {
        return "Rusty Sword";
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
        return Resources.Load<Sprite>("UI/Item Images/BlackSwordItem");
    }

    public override string GiveDiscresp()
    {
        return "Light ATK -> 35 DMG ; Heavy ATK -> 50 DMG";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEventItens>().InstantiateSword(GiveName());



        };
    }
}
