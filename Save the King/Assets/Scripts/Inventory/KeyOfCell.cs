using UnityEngine;
using System;
public class KeyOfCell : Item
{
    public override string GiveName()
    {
        return "KeyOfCell";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of Cell";
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
        return "A cold, corroded metal key, heavy with the despair of countless prisoners.";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            Debug.Log("Cell Key");
            
        };
    }
}
