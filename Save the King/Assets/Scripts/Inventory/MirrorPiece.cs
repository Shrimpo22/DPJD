using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MirrorPiece : MonoBehaviour
{
    public TMP_Text text;
    public void textActivate()
    {
        text.text = "(E) Grab Mirror Piece";
        text.gameObject.SetActive(true);
    }

    public void textClear()
    {
        text.gameObject.SetActive(false);
    }


    public void grabMirrorPiece()
    {
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.GetComponent<Inventory>().AddItem("GlassShard", 1);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
