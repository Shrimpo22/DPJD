using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandelabraLights : MonoBehaviour
{
    public bool lit = false;
    void OnMouseDown(){
        foreach (Transform child in transform){
            child.gameObject.SetActive(!child.gameObject.activeSelf);
            lit = child.gameObject.activeSelf;
        }

        gameObject.transform.parent.gameObject.GetComponent<CandelabraCombo>().CheckCombo();
    }
}
