using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MirrorInteractable : CamInteractable
{
    public bool isComplete;

    public GameObject objectNotSolved;
    public GameObject objectSolved;

    public int nrOfPiecesOn;
    public int totalPieces = 4;

    [SerializeField] public GameObject[] allPieces;


    public override void Start()
    {
        base.Start();
        objectNotSolved.SetActive(true);
        objectSolved.SetActive(false);
        nrOfPiecesOn = 0;
        isComplete = false;
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        base.Interact(inv, playerItems);
        inv.isLookingAtMirror = true;
    }

    public void AddPiece()
    {
        inventory.GetComponent<Inventory>().DropItemByName("GlassShard");
        allPieces[nrOfPiecesOn].SetActive(true);
        nrOfPiecesOn++;

        if (nrOfPiecesOn == totalPieces && !isComplete)
        {
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        isComplete = true;
        objectNotSolved.SetActive(false);
        objectSolved.SetActive(true);
    }


}
