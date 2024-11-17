using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Music : MonoBehaviour
{//aqui
    // Start is called before the first frame update
    public TMP_Text text;
    private bool hasBeenGrabbed;
    public void textActivate()
    {
        text.text = "(E) Grab Music Sheet";
        text.gameObject.SetActive(true);
    }

    public void textClear()
    {
        text.gameObject.SetActive(false);
    }


    public void grabMusicSheet()
    {
        if(!hasBeenGrabbed){
        hasBeenGrabbed = true;
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.GetComponent<Inventory>().AddItem("SheetMusic", 1);
        Destroy(gameObject);
        }
    }
}
