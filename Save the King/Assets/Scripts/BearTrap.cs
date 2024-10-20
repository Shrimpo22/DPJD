using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public Animator trapAnimator;  // Reference to the Animator component
    private bool isTriggered = false;  // Prevents the trap from being triggered multiple times

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("collided " + other.tag);
        // Check if the player enters the collider (assuming the player has a "Player" tag)
        if (other.CompareTag("Player") && !isTriggered)
        {
            // Trigger the "CloseTrap" animation
            trapAnimator.SetTrigger("CloseTrap");

            //other.hp -= x , onde hp é o hp do player e x é o damage da trap.

            // Se trapped entao nao anda.
            other.gameObject.GetComponent<PlayerMovement>().isTrapped = true;

            // Set the trap as triggered to prevent retriggering
            isTriggered = true;
             Debug.Log("inside if");
        }
    }
}
