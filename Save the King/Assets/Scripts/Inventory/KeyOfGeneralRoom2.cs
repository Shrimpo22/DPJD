using UnityEngine;

public class KeyOfGeneralRoom2 : Item
{
     public override string GiveName()
    {
        return "KeyOfGeneralRoom2";
    }

    public override string getNameWithSpaces()
    {
        return "Key Of General Room2";
    }
    public override int MaxStacks()
    {
        return 1;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/KeyOfGeneralRoom2");
    }

    public override string GiveDiscresp()
    {
        return "Uma chave misteriosa";
    }
}
