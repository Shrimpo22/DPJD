using UnityEngine;

public class KeyOfChest : Item
{
    public override string GiveName()
    {
        return "KeyOfChest";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of Chest";
    }
    public override int MaxStacks()
    {
        return 1;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/KeyOfChest");
    }

    public override string GiveDiscresp()
    {
        return "Uma chave misteriosa";
    }
}
