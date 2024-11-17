using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Torches : MonoBehaviour
{
    public TMP_Text text;
    private bool hasBeenGrabbed;
    public void textActivate()
    {
        text.text = "(E) Grab Torch";
        text.gameObject.SetActive(true);
    }

    public void textClear()
    {
        text.gameObject.SetActive(false);
    }


    public void grabTorch()
    {
        if(!hasBeenGrabbed){
        hasBeenGrabbed=true;
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.GetComponent<Inventory>().AddItem("Torch", 1);
        Destroy(gameObject);
    }
    }
}
