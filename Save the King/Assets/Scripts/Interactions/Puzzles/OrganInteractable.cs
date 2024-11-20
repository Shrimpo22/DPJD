using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrganInteractable : CamInteractable
{
    private int totalPieces = 1;
    private bool isComplete = false;

    public bool shownChalice = false;

    public GameObject paperMusic;

    public GameObject BookOpen;
    public GameObject BookClosed;

    public AudioSource audioSource;

    public override void Start()
    {
        base.Start();
        paperMusic.SetActive(false);
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        base.Interact(inv, playerItems);
        inv.isLookingAtOrgan = true;
    }
        
    public void AddPiece()
    {
        inventory.GetComponent<Inventory>().DropItemByName("SheetMusic");
        paperMusic.SetActive(true);
        isComplete = true;
        audioSource.Play();
        BookOpen.SetActive(true);
        BookClosed.SetActive(false);
        gameObject.tag = "Untagged";
        ExitCam();
    }

}
