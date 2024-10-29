using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public Animator trapAnimator;  // Reference to the Animator component
    public AudioSource trapSound;  // Reference to the AudioSource component
    private bool isTriggered = false;  // Prevents the trap from being triggered multiple times

    void Start()
    {
        // Ensure the AudioSource component is assigned
        if (trapSound == null)
        {
            trapSound = GetComponent<AudioSource>();
        }
    }
    void OnTriggerEnter(Collider other)
    {

        Debug.Log("collided " + other.tag);
        // Check if the player enters the collider (assuming the player has a "Player" tag)
        if (other.CompareTag("Player") && !isTriggered)
        {
            // Trigger the "CloseTrap" animation
            trapAnimator.SetTrigger("CloseTrap");

            // Play the sound
            trapSound.Play();

            //other.hp -= x , onde hp é o hp do player e x é o damage da trap.

            // Se trapped entao nao anda.
            other.gameObject.GetComponent<PlayerMovement>().isTrapped = true;

            // Set the trap as triggered to prevent retriggering
            isTriggered = true;
        }
    }
}
