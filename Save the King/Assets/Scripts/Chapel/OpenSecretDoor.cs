using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class OpenSecretDoor : MonoBehaviour
{

    GameObject player;
    public Camera myCamera;
    private Camera mainCamera;
    public GameObject freelockCamara;
    private CinemachineFreeLook freeLookComponent;
    private bool secretDoorOpen = false;
    public int rightPieces = 0;
    public int correctPiecesNeeded = 3;
    GameObject secretDoor;
    GameObject statueNear;


    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        freeLookComponent = freelockCamara.GetComponent<CinemachineFreeLook>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        secretDoor = this.gameObject;
    }

    public void NearMe(GameObject gm)
    {
        statueNear = gm;
    }

    public void AddItemToStatue(string nameOfItem)
    {
        if (statueNear != null)
        {
            statueNear.GetComponent<StatuesInteractable>().AddPiece(nameOfItem);
        }
    }

    public void UndoFunction()
    {
        if (statueNear != null)
        {
            statueNear.GetComponent<StatuesInteractable>().Undo();

        }
    }

    public void seeObject()
    {
        mainCamera.tag = "Untagged";
        myCamera.tag = "MainCamera";
        player.SetActive(false);

        mainCamera.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(true);

    }


    public void viewOpening()
    {

        if (secretDoor.GetComponent<Door>() != null)
            secretDoorOpen = true;
        seeObject();
        this.gameObject.GetComponent<Door>().Open(30);
        GameObject[] allStatues = GameObject.FindGameObjectsWithTag("Statue");
        foreach (GameObject statue in allStatues)
        {
            statue.tag = "Untagged";
        }



    }

    public void exitCam()
    {
        player.SetActive(true);
        Time.timeScale = 1;
        if (freeLookComponent != null)
        {
            freeLookComponent.enabled = true;
        }
        mainCamera.tag = "MainCamera";
        myCamera.tag = "Untagged";
        mainCamera.gameObject.SetActive(true);
        myCamera.gameObject.SetActive(false);

    }

    public void sum()
    {
        rightPieces++;
    }

    public void subtract()
    {
        if (rightPieces > 0)
            rightPieces--;
    }

    void Update()
    {
        if (!this.gameObject.GetComponent<Door>().IsClosed && secretDoorOpen)
        {
            waitForDoor();
            exitCam();
            secretDoorOpen = false;
        }
    }

    IEnumerator waitForDoor()
    {
        yield return new WaitForSecondsRealtime(6);
    }

}
