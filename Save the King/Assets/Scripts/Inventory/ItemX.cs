using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemX : Item
{
    public override string getNameWithSpaces()
    {
        return "Item XXX";
    }

    public override string GiveDiscresp()
    {
        return "An Item To Explain the rework";
    }

    public override string GiveName()
    {
        return "ItemX";
    }

    public override int Stats()
    {
        return 0;
    }

    public override int MaxStacks()
    {
        return 1;
    }

    public override Action UseFunc()
    {
        return () => Debug.Log("Dunno");
    }
}
