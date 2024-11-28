using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CandleHolderInteractable : CamInteractable
{
    public int nrOfPiecesOn;
    public Camera drawerCamera;
    public GameObject drawer;
    public AudioSource audioSource;

    [SerializeField] public GameObject[] allPieces;
    public float moveDistance = 0.3f;  // Distance the drawer should move when opened
    public float speed = 1f;  // Speed of the movement

    private Vector3 initialPosition;  // The starting position of the drawer
    private Vector3 targetPosition;   // The target position of the drawer when fully opened
    private bool isOpening = false;   // Flag to indicate whether the drawer is opening
    private float lerpTime = 0f;      // Controls the interpolation timea

    public override void Start()
    {
        base.Start();
        nrOfPiecesOn = 0;

        initialPosition = drawer.transform.position;  // Store the initial position of the drawer
        targetPosition = initialPosition + drawer.transform.forward * moveDistance;  // Set the target position
        Debug.Log(initialPosition);
        Debug.Log(targetPosition);
    }

    void Update()
    {
        if (isOpening)
        {
            lerpTime += Time.deltaTime * speed;  // Increase the lerp time based on speed
            drawer.transform.position = Vector3.Lerp(initialPosition, targetPosition, lerpTime);

            // Stop the drawer movement when it reaches the target position
            if (lerpTime >= 1f)
            {
                lerpTime = 1f;  // Clamp the lerpTime
                isOpening = false;  // Stop further movement
            }
        }
    }

    // Call this method to start opening the drawer
    public void OpenDrawer()
    {
        if (!isOpening)
        {
            isOpening = true;
            lerpTime = 0f;  // Reset the lerpTime so the movement starts from the beginning
        }
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
            ExitCam();
            LookAtDrawer();
            OpenDrawer();
        }
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        base.Interact(inv, playerItems);
        inv.isLookingAtCandleHolder = true;
    }
   public void LookAtDrawer(){
        mainCamera.tag="Untagged";
        drawerCamera.tag="MainCamera";
        mainCamera.gameObject.SetActive(false);
        drawerCamera.gameObject.SetActive(true);

        StartCoroutine(PlaySoundAndExit());
    }

    IEnumerator PlaySoundAndExit()
    {
        Debug.Log("Opening Drawer");

        if (audioSource.clip != null)
        {
                audioSource.Play();

                yield return new WaitForSeconds(2);

                drawerCamera.tag="Untagged";
                mainCamera.tag="MainCamera";
                drawerCamera.gameObject.SetActive(false);
                mainCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to bookSource.");
        }

        Debug.Log("Finished");
    }

}
