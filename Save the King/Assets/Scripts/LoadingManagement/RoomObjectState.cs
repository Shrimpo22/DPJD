using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObjectState : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool initialActiveState;
    private bool isLocked;
    private bool isAvailable;

    void Awake()
    {

    }

    // Save the initial state of the object
    public void SaveInitialState()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialActiveState = gameObject.activeSelf;

        Door door = gameObject.GetComponent<Door>();
        if (door != null)
        {
            isLocked = door.IsLocked;
        }
        
        OpenableInteractable oi = gameObject.GetComponent<OpenableInteractable>();
        if (oi != null)
        {
            isAvailable = oi.available;
        }
    }

    // Reset the object to its initial state
    public void ResetToInitialState()
    {
        Debug.Log("object name : " + gameObject.name);
        Door door = gameObject.GetComponent<Door>();
        if (door != null)
        {
            Canvas canvas = gameObject.GetComponent<OpenableInteractable>().interactCanvas;
            if (canvas != null)
            {
                canvas.enabled = true;
            }
            door.IsClosed = true;
            door.IsLocked = isLocked;
        }

        OpenableInteractable oi = gameObject.GetComponent<OpenableInteractable>();
        if (oi != null)
        {
            oi.available = isAvailable;
        }


        gameObject.SetActive(initialActiveState);
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        DoubleDoor dd = gameObject.GetComponent<DoubleDoor>();
        if (dd != null)
        {
            Debug.Log("im inside doouble door");
            //dd.IsClosed = true;
            dd.reverseOpenDoors();
            dd.IsLocked = isLocked;
        }

        BearTrap bt = gameObject.GetComponent<BearTrap>();
        if(bt != null)
        {
            bt.trapAnimator.SetTrigger("OpenTrap");
            bt.isTriggered = false;

        }

        OpenChest chest = gameObject.GetComponent<OpenChest>();
        if ( chest != null)
        {
            gameObject.layer = 7;
            chest.isRotating = false;
            chest.IsClosed = false;
            chest.gameObject.GetComponent<Collider>().enabled = true;
            chest.gameObject.GetComponent<OpenableInteractable>().available = true;
        }

    }
}
