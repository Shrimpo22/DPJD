using UnityEngine;

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
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/KeyOfCell");
    }

    public override string GiveDiscresp()
    {
        return "Uma chave misteriosa";
    }
}
