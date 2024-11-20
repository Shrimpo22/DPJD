using UnityEngine;
using System;

public class Sword : Item
{
    public override string GiveName()
    {
        return "Sword";
    }

    public override string getNameWithSpaces()
    {
        return "Sword";
    }
    public override int MaxStacks()
    {
        return 1;
    }
    public override int Stats()
    {
        //Adicionar o que se quer para statisticas
        return 0;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/SwordItem");
    }

    public override string GiveDiscresp()
    {
        return "Sword";
    }
    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Sword");
            
        };
    }
}
