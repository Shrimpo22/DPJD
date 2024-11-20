using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleInteractable : CamInteractable
{
    public bool isComplete;

    public GameObject objectNotSolved;
    public GameObject objectSolved;

    public int nrOfPiecesOn;
    public int totalPieces = 4;

    [SerializeField] public GameObject[] allPieces;


    public override void Start(){
        objectNotSolved.SetActive(true);
        objectSolved.SetActive(false);
        nrOfPiecesOn = 0 ;

        interactCanvas.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(false);
        
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        isLooking = false;
        isComplete = false;
    }

    public void AddPiece(){
       inventory.GetComponent<Inventory>().DropItemByName("GlassShard");
       allPieces[nrOfPiecesOn].SetActive(true);
       nrOfPiecesOn++;

       if(nrOfPiecesOn == totalPieces && !isComplete) {
            SolvePuzzle();
       }
    }

    private void SolvePuzzle(){
        isComplete=true;
        objectNotSolved.SetActive(false);
        objectSolved.SetActive(true);
    }

    
}
