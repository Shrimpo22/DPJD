using UnityEngine;
using System;

public class Potions : Item
{
    
    public override string GiveName()
    {
        return "Potions";
    }

    public override string getNameWithSpaces()
    {
        return "Potions";
    }
    public override int MaxStacks()
    {
        return 10;
    }

    public override int Stats()
    {   
        //Adicionar o que se quer aumentar na vida
        return 10;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/Potions");
    }

    public override string GiveDiscresp()
    {
        return "Potions to increase health";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Potions");
            
        };
    }
}
