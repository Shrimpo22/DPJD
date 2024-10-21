using UnityEngine;

public class KeyOfKingThrone  : Item
{
    public override string GiveName()
    {
        return "KeyOfKingThrone";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of King Throne";
    }
    public override int MaxStacks()
    {
        return 1;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/KeyOfKingThrone");
    }

    public override string GiveDiscresp()
    {
        return "Uma chave misteriosa";
    }
}