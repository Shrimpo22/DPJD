using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Candles : MonoBehaviour
{
    public TMP_Text text;
    private bool hasBeenGrabbed;

    public void textActivate()
    {
        text.text = "(E) Grab Candle";
        text.gameObject.SetActive(true);
    }

    public void textClear()
    {
        text.gameObject.SetActive(false);
    }


    public void grabCandle()
    {
        if(!hasBeenGrabbed){
        hasBeenGrabbed=true;
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.GetComponent<Inventory>().AddItem("Candle", 1);
        Destroy(gameObject);
    }
    }
}
