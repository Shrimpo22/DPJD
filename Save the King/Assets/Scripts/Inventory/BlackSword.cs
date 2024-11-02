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
        return "Black Sword";
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
        return Resources.Load<Sprite>("UI/Item Images/KeyOfCell");
    }

    public override string GiveDiscresp()
    {
        return "Damage -> 10 ; Speed -> 2";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEventItens>().InstantiateSword(GiveName());



        };
    }
}
