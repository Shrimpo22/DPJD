using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class GreatSword : Item
{
    public override string GiveName()
    {
        return "GreatSword";
    }

    public override string getNameWithSpaces()
    {
        return "Greatsword";
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
        return Resources.Load<Sprite>("UI/Item Images/GreatSwordItem");
    }

    public override string GiveDiscresp()
    {
        return "Light ATK -> 65 DMG ; Heavy ATK -> 80 DMG";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEventItens>().InstantiateSword(GiveName());



        };
    }
}
