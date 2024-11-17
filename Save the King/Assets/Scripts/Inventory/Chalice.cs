using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalice : Item
{
    public override string GiveName()
    {
        return "Chalice";
    }

    public override string getNameWithSpaces()
    {
        return "Chalice";
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
        return Resources.Load<Sprite>("UI/Item Images/MapItem");
    }

    public override string GiveDiscresp()
    {
        return "A shinny chalice... looks like it would fit a statue's hand";
    }
    public override Action UseFunc()
    {
        return () =>
        { // Erro aqui. Sugestºao : Ir ao player(pela tag) e dele chamar o collider da estatua mais proxima dele (GetComponent<Statues>().addPiece(GiveName()))
           GameObject.FindGameObjectWithTag("Statue").GetComponent<Statues>().addPiece(GiveName());
        };
    }
}
