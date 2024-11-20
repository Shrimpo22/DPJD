using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStealthInteractable : Interactable
{
    PlayerMovement playerMovement;
    private bool isAvailable = false;
    public override void Start()
    {
        base.Start();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        playerMovement.StealthAttack(gameObject);
        interactCanvas.gameObject.SetActive(false);
        gameObject.layer = 0;
    }

    public override void EnterRange()
    {
        if (isAvailable)
            base.EnterRange();
    }

    public void Update()
    {
        isAvailable = playerMovement.isCrouching && playerMovement.isSwordEquipped && !playerMovement.IsDetected();
    }

}
