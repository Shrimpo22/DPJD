using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    // Implementing the properties from IDoor interface
    [SerializeField] private bool isLocked; // Serialize private field for IsLocked
    [SerializeField] private bool isClosed = true; // Serialize private field for IsClosed

    
    public bool IsLocked 
    { 
        get { return isLocked; } 
        set { isLocked = value; }
    }

    public bool IsClosed 
    { 
        get { return isClosed; } 
        set { isClosed = value; }
    }

    public TMP_Text TextDoor;
    public int Angle = 1;
    public float RotationSpeed { get; set; } = 90f;
    public AudioClip LockedSound { get; set; }
    public AudioClip UnlockedOpenSound { get; set; }
    public Key.KeyType KeyType { get; set; }

    // Additional private fields for internal use
    private float currentRotation = 0f;
    public float targetRotation = 0f;
    private AudioSource audioSource;

    // Start method
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (TextDoor != null) TextDoor.color = Color.white;
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Method for key type retrieval
    private Key.KeyType GetKeyType()
    {
        return KeyType;
    }

    // Method to activate the door text
    public void TextActivate()
    {
        TextDoor.text = "(E) Open Door";
        TextDoor.gameObject.SetActive(true);
    }

    // Method to clear the door text
    public void TextClear()
    {
        TextDoor.gameObject.SetActive(false);
    }

    // Method to open the door
    public void OpenDoor(float rotationWanted){
        RotationSpeed = rotationWanted;
        targetRotation = 90f * Angle;
        audioSource.clip = UnlockedOpenSound;
        audioSource.Play();
    }
    
    public void OpenDoor(GameObject player)
    {
        if (!IsLocked && IsClosed)
        {
            targetRotation = 90f * Angle;
            audioSource.clip = UnlockedOpenSound;
            audioSource.Play();
            TextClear();
            player.GetComponent<PlayerEventItens>().isNearDoor = false;
        }
        else if (IsLocked)
        {
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            if (player.GetComponent<PlayerEventItens>().listOfKeys.Count > 0 && player.GetComponent<PlayerEventItens>().listOfKeys.Contains(GetKeyType()))
            {
                IsLocked = false;
                player.GetComponent<PlayerEventItens>().RemoveKey(GetKeyType());
                inventory.GetComponent<Inventory>().DropItemByName(GetKeyType().ToString());
                OpenDoor(player);
            }
            else
            {
                audioSource.clip = LockedSound;
                audioSource.Play();
                TextDoor.text = "Locked";
            }
        }
    }

    // Method called when the player exits the trigger area
    public void OnTriggerExit(Collider other)
    {
        TextClear();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerEventItens>().isNearDoor = false;
    }

    // Update method to handle door rotation
    public void Update()
    {
        if (!IsLocked && IsClosed && currentRotation < targetRotation * Angle)
        {
            float rotationThisFrame = RotationSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, -rotationThisFrame * Angle, 0), Space.Self);
            currentRotation += rotationThisFrame;

            if (currentRotation >= targetRotation * Angle)
            {
                targetRotation = 0f;
                currentRotation = 0f;
                IsClosed = false;
            }
        }
    }
}
