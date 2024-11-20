using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class MapInteractable : CamInteractable
{
    public bool isComplete;

    public int nrOfPiecesOn;
    public int totalPieces = 5;
    private string finalAnswer;
    public TMP_Text respostaNumero;

    [SerializeField] public GameObject[] allPieces;


    public override void Start(){
        nrOfPiecesOn = 0 ;

        interactCanvas.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(false);
        
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        isLooking = false;
        isComplete = false;
    }

    public void getTextFinalAnswer (string number){
        finalAnswer = number;
    }

    public void AddPiece(){
       inventory.GetComponent<Inventory>().DropItemByName("MapPiece");
       allPieces[nrOfPiecesOn].SetActive(false);
       nrOfPiecesOn++;

       if(nrOfPiecesOn == totalPieces && !isComplete) {
            SolvePuzzle();
       }
    }
    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        base.Interact(inv, playerItems);
        inv.isLookingAtMap = true;
    }

    private void SolvePuzzle(){
        isComplete=true;
        Debug.Log(finalAnswer);
        respostaNumero.text = finalAnswer;
        respostaNumero.gameObject.SetActive(true);

    }

    
}
