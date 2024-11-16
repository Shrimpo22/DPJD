using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePainting : MonoBehaviour
{
    public PuzzleInteractable pi;
    public PaintingInteractable pi2;
    // Update is called once per frame
    void Update()
    {
        if(pi.isComplete){
            pi2.active = true;
        }
    }
}
