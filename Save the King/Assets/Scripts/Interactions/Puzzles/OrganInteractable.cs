using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrganInteractable : CamInteractable
{

    private bool isComplete = false;

    public GameObject paperMusic;

    public Camera bookCamera;
    public GameObject BookOpen;
    public GameObject BookClosed;

    public AudioSource audioSource;
    public AudioSource bookSource;

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
       
        gameObject.tag = "Untagged";
        ExitCam();

        LookAtBook();
    }

    public void LookAtBook(){
        mainCamera.tag="Untagged";
        bookCamera.tag="MainCamera";
        mainCamera.gameObject.SetActive(false);
        bookCamera.gameObject.SetActive(true);

        StartCoroutine(PlaySoundAndExit());
    }

    IEnumerator PlaySoundAndExit()
    {

        if (bookSource.clip != null)
        {
                yield return new WaitForSeconds(1); // waits before opening

                bookSource.Play();
                BookOpen.SetActive(true);
                BookClosed.SetActive(false);

                yield return new WaitForSeconds(5); // waits before leaving camera

                mainCamera.tag="MainCamera";
                bookCamera.tag="Untagged";
                bookCamera.gameObject.SetActive(false);
                mainCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to bookSource.");
        }

        Debug.Log("Finished");
    }

}
