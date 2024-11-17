using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece : Item
{
    public override string GiveName()
    {
        return "MapPiece";
    }

    public override string getNameWithSpaces()
    {
        return "Map Piece";
    }
    public override int MaxStacks()
    {
        return 5;
    }
    public override int Stats()
    {
        return 0;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/MapItem");
    }

    public override string GiveDiscresp()
    {
        return "A ripped part of a paper... i wonder from where it's missing..";
    }
    public override Action UseFunc()
    {
        return () =>
        {
           GameObject.FindGameObjectWithTag("Map").GetComponent<MapInteractable>().AddPiece();
        };
    }
}

