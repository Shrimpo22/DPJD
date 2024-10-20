using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDial : MonoBehaviour
{
    private Quaternion[] rotations = new Quaternion[8];
    private AudioSource audioSource;

    public bool canMove = false;
    private int currIndex;
    void Start()
    {
        fillRotations();
        currIndex =0;
        transform.localRotation = rotations[0];
        audioSource = GetComponent<AudioSource>();
        canMove=true;
        
    }

    private void fillRotations()
    {
       
        rotations[0] = Quaternion.Euler(0f, 0f, -22f);
        rotations[1] = Quaternion.Euler(0f, 0f, 23f);
        rotations[2] = Quaternion.Euler(0f, 0f, 68f);
        rotations[3] = Quaternion.Euler(0f, 0f, 113f);
        rotations[4] = Quaternion.Euler(0f, 0f, 158f);
        rotations[5] = Quaternion.Euler(0f, 0f, 203f);
        rotations[6] = Quaternion.Euler(0f, 0f, 248f);
        rotations[7] = Quaternion.Euler(0f, 0f, 293f);
        
    }

    private void OnMouseDown() {
        if(canMove){
        currIndex++; 
        if(currIndex>7) currIndex=0;
        audioSource.Play();
        transform.localRotation = rotations[currIndex];
        }
    }

    public int getNr(){
        return currIndex;
    }
}
