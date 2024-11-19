using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChaliceObj : MonoBehaviour
{

     public TMP_Text text;
    private bool hasBeenGrabbed;

    public void textActivate()
    {
        text.text = "(E) Grab Chalice";
        text.gameObject.SetActive(true);
    }

    public void textClear()
    {
        text.gameObject.SetActive(false);
    }


    public void grabChalice()
    {
        if(!hasBeenGrabbed){
        hasBeenGrabbed=true;
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.GetComponent<Inventory>().AddItem("Chalice", 1);
        Destroy(gameObject);
    }
    }
}
