using UnityEngine;
using System;

public class GlassShard : Item
{
    public override string GiveName()
    {
        return "GlassShard";
    }

    public override string getNameWithSpaces()
    {
        return "Glass Shard";
    }
    public override int MaxStacks()
    {
        return 4;
    }
    public override int Stats()
    {
        return 0;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/KeyOfKitchen");
    }

    public override string GiveDiscresp()
    {
        return "A shard of glass, you almost think you see... another world reflected. I wonder from where it is...";
    }
    public override Action UseFunc()
    {
        return () =>
        {
            GameObject.FindGameObjectWithTag("MirrorTable").GetComponent<MirrorTableCam>().addPiece();
        };
    }
}
