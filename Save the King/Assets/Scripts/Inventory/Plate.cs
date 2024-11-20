using UnityEngine;
using System;

public class Plate : Item
{
    public override string GiveName()
    {
        return "Plate";
    }

    public override string getNameWithSpaces()
    {
        return "Plate";
    }
    public override int MaxStacks()
    {
        return 1;
    }

    public override int Stats()
    {   
        return 1;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/Plate");
    }

    public override string GiveDiscresp()
    {
        return "A delicious meal";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().DropItemByName(GiveName());
            GameObject.FindGameObjectWithTag("NPCVIBES").GetComponent<EnemyRecipeInteractable>().useItem();

        };
    }
}
