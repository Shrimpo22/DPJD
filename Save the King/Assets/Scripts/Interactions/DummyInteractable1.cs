using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DummyInteractable : Interactable
{
    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        Debug.Log("Interacted!");
        inv.AddItem("DummyItem",1);
        ClearAndDestroy(playerItems);
    }
}
