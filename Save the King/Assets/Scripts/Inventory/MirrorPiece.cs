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
        Debug.Log("ASDDDDDDDDDDsAAAAAAAAA");
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        Debug.Log("AAAAAAAAA" + inventory);
        inventory.GetComponent<Inventory>().AddItem("Glass Shard", 1);
        Debug.Log("BBBBB");
        this.gameObject.SetActive(false);
    }
}
