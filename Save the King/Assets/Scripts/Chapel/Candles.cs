using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Candles : MonoBehaviour
{
    public TMP_Text text;
    public void textActivate()
    {
        text.text = "(E) Grab Candle";
        text.gameObject.SetActive(true);
    }

    public void textClear()
    {
        text.gameObject.SetActive(false);
    }


    public void grabMirrorPiece()
    {
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.GetComponent<Inventory>().AddItem("Candles", 1);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
