using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CandleHolderInteractable : CamInteractable
{
    public int nrOfPiecesOn;

    public GameObject drawer;
    public AudioSource audioSource;

    [SerializeField] public GameObject[] allPieces;

    public override void Start()
    {
        base.Start();
        nrOfPiecesOn = 0;

    }

    public void AddPiece()
    {
        inventory.GetComponent<Inventory>().DropItemByName("Candle");
        allPieces[nrOfPiecesOn].SetActive(true);
        nrOfPiecesOn++;
        if (nrOfPiecesOn == 2)
        {
            gameObject.tag = "Untagged";
            this.gameObject.GetComponent<Collider>().enabled = false;
            drawer.transform.position = drawer.transform.position + drawer.transform.forward * 0.3f;
            StartCoroutine(PlaySoundAndExit());
        }
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        base.Interact(inv, playerItems);
        inv.isLookingAtCandleHolder = true;
    }
    IEnumerator PlaySoundAndExit()
    {
        Debug.Log("Opening Drawer");

        if (audioSource.clip != null)
        {
            audioSource.Play();
            ExitCam();
            yield return new WaitForSeconds(audioSource.clip.length + 1f);


        }
        else
        {
            Debug.LogWarning("No audio clip assigned to AudioSource.");
        }

        Debug.Log("Finished");
    }
}
