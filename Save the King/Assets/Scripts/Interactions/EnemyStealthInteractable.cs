using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStealthInteractable : Interactable
{
    PlayerMovement playerMovement;
    public override void Start()
    {
        base.Start();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        gameObject.GetComponent<NavMeshAgent>().speed = 0;
        playerMovement.StealthAttack(gameObject);
        interactCanvas.gameObject.SetActive(false);
        gameObject.layer = 0;
    }

    public override void EnterRange()
    {
        base.EnterRange();
    }

}
