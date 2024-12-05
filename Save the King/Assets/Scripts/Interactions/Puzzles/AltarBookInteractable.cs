using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarBookInteractable : CamInteractable
{

    public Camera playerCam;

    public override void Start()
    {
        base.Start();
    }

    public override void ExitCam()
    {
        
        player.SetActive(true);
        playerCam.tag = "MainCamera";
        myCamera.tag="Untagged";
        myCamera.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
        isLooking = false;
       
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        base.Interact(inv, playerItems);
        
        player.SetActive(false);

        myCamera.tag = "MainCamera";
        playerCam.tag = "Untagged";
        playerCam.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(true);
        isLooking = true;
        

    }

}
