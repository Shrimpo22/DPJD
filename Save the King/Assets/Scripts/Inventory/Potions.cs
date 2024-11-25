using UnityEngine;
using System;

public class Potions : Item
{
    
    public AudioSource audioSource;
    public override string GiveName()
    {
        return "Potions";
    }

    public override string getNameWithSpaces()
    {
        return "Potions";
    }
    public override int MaxStacks()
    {
        return 10;
    }

    public override int Stats()
    {   
       
        return 50;
    }
    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/Potions");
    }

    public override string GiveDiscresp()
    {
        return "Potions to increase health";
    }

    public override Action UseFunc()
    {
        return () =>
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/PotionAudio");
            if (clip != null)
            {
                AudioSource audioSource = new GameObject("PotionAudioSource").AddComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.Play();
                GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
                inventory.GetComponent<Inventory>().DropItemByName(GiveName());
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    if (playerHealth.currentHealth < playerHealth.maxHealth)
                    {
                        
                        playerHealth.currentHealth = Mathf.Min(
                            playerHealth.currentHealth + Stats(), 
                            playerHealth.maxHealth
                        );
                    }
                }
                else
                {
                    Debug.LogWarning("PlayerHealth component not found on the player!");
                }

                Debug.Log("Potions used");
                    
               
            }
            Debug.Log("Potions");
        };
    }
}
