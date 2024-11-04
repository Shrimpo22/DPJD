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
        //Adicionar o que se quer aumentar na vida
        return 10;
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
                
                
               
            }
            Debug.Log("Potions");
        };
    }
}
