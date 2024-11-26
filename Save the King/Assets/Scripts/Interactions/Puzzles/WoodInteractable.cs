using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodInteractable : CamInteractable
{
    // Start is called before the first frame update
    public GameObject[] woods;

    private float fadeSpeed = 1/2f;
        public AudioSource audioSource;

    public override void Start()
    {
        base.Start();
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        base.Interact(inv, playerItems);
        inv.isLookingAtWood = true;
    }

    public void AddPiece()
    {
        inventory.GetComponent<Inventory>().closeInventory();
        inventory.GetComponent<Inventory>().DropItemByName("Crowbar");
        foreach(GameObject wood in woods){
            StartCoroutine(Wait());
            Destroy(wood);
        }
        ExitCam();
        ClearAndDestroy(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEventItens>());
    }

     IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(4); 
    }

}

