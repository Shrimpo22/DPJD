using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : Item
{
    // Start is called before the first frame update
    public override string GiveName()
    {
        return "Candle";
    }

    public override string getNameWithSpaces()
    {
        return "Candle";
    }
    public override int MaxStacks()
    {
        return 2;
    }
    public override int Stats()
    {
        return 0;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/CandleItem");
    }

    public override string GiveDiscresp()
    {
        return "A simple candle";
    }
    public override Action UseFunc()
    {
        return () =>
        {
           GameObject.FindGameObjectWithTag("CandleHolder").GetComponent<CandleHolderInteractable>().AddPiece();
        };
    }
}
