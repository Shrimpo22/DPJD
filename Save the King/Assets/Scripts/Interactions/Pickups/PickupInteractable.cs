using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PickupInteractable : Interactable
{
    public ItemType itemToGive;
    public int amount;
    public bool pickedUp = false;
    public bool clickable = false;
    public AudioSource audioSource;
    public AudioClip pickupSound;
    public override void Start()
    {
        interactCanvas.gameObject.SetActive(false);
        if (audioSource == null)
        {
            AudioMixer audioMixer = Resources.Load<AudioMixer>("Sounds/AudioMixer");
            string groupName = "SFX";
            AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(groupName);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = groups[0];

        }
        if (pickupSound == null)
            pickupSound = Resources.Load<AudioClip>("Sounds/Puma");
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        if (!pickedUp)
        {
            pickedUp = true;
            inv.AddItem(itemToGive.ToString(), amount);
            interactCanvas.gameObject.SetActive(false);
            if (pickupSound != null)
            {
                audioSource.clip = pickupSound;
                audioSource.Play();
                ClearAndDestroy(playerItems, pickupSound.length);
            }
            else
            {
                ClearAndDestroy(playerItems, 0);
            }

        }
    }

    public void OnMouseDown()
    {
        if (clickable)
        {
            Interact(GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>(), Resources.FindObjectsOfTypeAll<PlayerEventItens>().FirstOrDefault());
        }
    }
}
