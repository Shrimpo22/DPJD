using UnityEngine;

public class KeyOfKitchen : Item
{
    public override string GiveName()
    {
        return "KeyOfKitchen";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of Kitchen";
    }
    public override int MaxStacks()
    {
        return 1;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/KeyOfKitchen");
    }

    public override string GiveDiscresp()
    {
        return "Uma chave misteriosa";
    }
}
