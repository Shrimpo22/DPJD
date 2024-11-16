using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorInteractable : Interactable, IDoor
{
    [SerializeField] private bool isLocked; // Serialize private field for IsLocked
    [SerializeField] private bool isClosed = true; // Serialize private field for IsClosed

    public bool IsLocked 
    { 
        get { return isLocked; } 
        set { isLocked = value; }
    }

    public bool IsClosed 
    { 
        get { return isClosed; } 
        set { isClosed = value; }
    }

    public float RotationSpeed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        Debug.Log("Interacted!");
        inv.AddItem("DummyItem",1);
        playerItems.ClearClosestInteractable();
        Destroy(gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
    }

    public void OpenDoor(GameObject player)
    {
    }

    public void TextActivate()
    {
    }

    public void TextClear()
    {
    }

    public void Update()
    {
    }
}
