using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatuesInteractable : CamInteractable
{
    public GameObject button;

    [SerializeField]
    public string correctPiece;
    private bool isRightPiece = false;
    private bool isEmpty = true;
    private bool isComplete = false;
    private bool hasObject = false;

    public GameObject secretDoor;

    private string object_name;

    public GameObject chalice;
    public GameObject torch;



    public override void Start()
    {
        base.Start();
        button.SetActive(false);
        chalice.SetActive(false);
        torch.SetActive(false);

    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        inv.isLookingAtStatue = true;
        base.Interact(inv, playerItems);
        if (hasObject)
            button.SetActive(true);
        secretDoor.GetComponent<OpenSecretDoor>().NearMe(this.gameObject);
    }

    public void Undo()
    {
        if (!string.IsNullOrWhiteSpace(object_name))
        {
            RemovePiece();
        }
    }

    public void FindtheStatue(string name)
    {
        secretDoor.GetComponent<OpenSecretDoor>().AddItemToStatue(name);
    }

    public void AddPiece(string name)
    {
        object_name = name;
        inventory.GetComponent<Inventory>().DropItemByName(object_name);
        hasObject = true;
        if (object_name == "Chalice")
        {
            chalice.SetActive(true);
        }
        else
        {
            torch.SetActive(true);
        }
        button.SetActive(true);

        if (object_name == correctPiece)
        {
            isRightPiece = true;
            secretDoor.GetComponent<OpenSecretDoor>().sum();
            if (secretDoor.GetComponent<OpenSecretDoor>().rightPieces == secretDoor.GetComponent<OpenSecretDoor>().correctPiecesNeeded)
            {
                ExitCam();
                secretDoor.GetComponent<OpenSecretDoor>().viewOpening();
            }
        }

    }

    public override void ExitCam()
    {
        base.ExitCam();
        button.SetActive(false);
    }

    public void RemovePiece()
    {
        chalice.SetActive(false);
        torch.SetActive(false);
        hasObject = false;
        inventory.GetComponent<Inventory>().AddItem(object_name, 1);
        object_name = null;
        button.SetActive(false);
        Debug.Log(button.activeSelf);
        if (isRightPiece)
        {
            isRightPiece = false;
            secretDoor.GetComponent<OpenSecretDoor>().subtract();
        }
    }

    private void SolvePuzzle()
    {
        isComplete = true;
    }


}
