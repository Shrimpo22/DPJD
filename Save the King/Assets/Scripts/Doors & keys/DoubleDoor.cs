using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoubleDoor : MonoBehaviour, IDoor
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
    public AudioClip LockedSound;
    public AudioClip UnlockedOpenSound;
    public Key.KeyType KeyType;

    // Additional variables for DoubleDoor functionality
    public GameObject leftDoor;
    public GameObject rightDoor;

    private Door leftDoorComp;
    private Door rightDoorComp;

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        TextDoor.color = Color.white;

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        AddDoorComps();
    }

    private void AddDoorComps()
    {
        leftDoorComp = leftDoor.AddComponent<Door>();
        rightDoorComp = rightDoor.AddComponent<Door>();
    }

    private Key.KeyType GetKeyType()
    {
        return KeyType;
    }

    // IDoor method implementations

    public void TextActivate()
    {
        TextDoor.text = "(E) Open Door";
        TextDoor.gameObject.SetActive(true);
    }

    public void TextClear()
    {
        TextDoor.gameObject.SetActive(false);
    }

    // Double-door specific method
    private void OpenDoors()
    {
        leftDoorComp.Angle = -1; // Left door opens in the opposite direction
        rightDoorComp.Angle = 1; // Right door opens in the opposite direction
        leftDoorComp.targetRotation = -90f;
        rightDoorComp.targetRotation = 90f;
        IsClosed = false;
    }

    public void OpenDoor(GameObject player)
    {
        if (!IsLocked && IsClosed)
        {
            audioSource.clip = UnlockedOpenSound;
            audioSource.Play();
            TextClear();
            player.GetComponent<PlayerEventItens>().isNearDoor = false;
            OpenDoors();
        }
        else if (IsLocked)
        {
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            if (player.GetComponent<PlayerEventItens>().listOfKeys.Count > 0 &&
                player.GetComponent<PlayerEventItens>().listOfKeys.Contains(GetKeyType()))
            {
                Debug.Log("A");
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

    public void OnTriggerExit(Collider other)
    {
        TextClear();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerEventItens>().isNearDoor = false;
    }

    public void Update()
    {
    }

}
