using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public Animator trapAnimator;  // Reference to the Animator component
    public AudioSource trapSound;  // Reference to the AudioSource component
    private bool isTriggered = false;  // Prevents the trap from being triggered multiple times
    public int trapDamage = 20;  // Initial damage dealt by the trap
    public int bleedingDamage = 3;  // Damage dealt per tick while bleeding
    public float bleedingInterval = 1.0f;  // Time interval between each bleed tick (in seconds)

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
        if (other.CompareTag("Player") && !isTriggered && this.transform.position.y <0.3f)
        {

            // Trigger the "CloseTrap" animation
            trapAnimator.SetTrigger("CloseTrap");

            // Play the sound
            trapSound.Play();

             // Access the player's health and apply trap damage
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();

            // Se trapped entao nao anda.
            playerMovement.isTrapped = true;

            // Set the trap as triggered to prevent retriggering
            isTriggered = true;

            if (playerHealth != null)
            {
                // Deal initial trap damage
                playerHealth.TakeDamage(trapDamage);

                // Start bleeding effect
                StartCoroutine(ApplyBleeding(playerHealth, playerMovement));
            }

        }
    }

    // Coroutine to apply bleeding damage over time
    IEnumerator ApplyBleeding(PlayerHealth playerHealth,  PlayerMovement playerMovement)
    {
        Debug.Log("is trapped? " + playerMovement.isTrapped);
        // Aplica dano enquanto o jogador estiver preso
        while (playerMovement.isTrapped == true && playerHealth.currentHealth > 0)
        {
            Debug.Log("taking damage");
            playerHealth.currentHealth -= bleedingDamage; // Caso nao queiramos animacao de takedamage()
            if (playerHealth.currentHealth <= 0)
            {
                playerHealth.TakeDamage(bleedingDamage);
            }
            //playerHealth.TakeDamage(bleedingDamage); // Causa dano de sangramento
            yield return new WaitForSeconds(bleedingInterval); // Espera pelo próximo tic de sangramento
        }

        

    } 
}

