using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Torches : MonoBehaviour
{
    public TMP_Text text;
    public void textActivate()
    {
        text.text = "(E) Grab Torch";
        text.gameObject.SetActive(true);
    }

    public void textClear()
    {
        text.gameObject.SetActive(false);
    }


    public void grabMirrorPiece()
    {
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.GetComponent<Inventory>().AddItem("Torches", 1);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
