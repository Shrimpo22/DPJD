using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoubleDoor : MonoBehaviour, IOpenable
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

    // Double-door specific method
    private void OpenDoors()
    {
        leftDoorComp.Angle = -1; // Left door opens in the opposite direction
        rightDoorComp.Angle = 1; // Right door opens in the opposite direction
        leftDoorComp.targetRotation = -90f;
        rightDoorComp.targetRotation = 90f;
        IsClosed = false;
    }

    public void Open(GameObject player, Canvas canvas)
    {
        if (!IsLocked && IsClosed)
        {
            audioSource.clip = UnlockedOpenSound;
            audioSource.Play();
            canvas.enabled = false;
            player.GetComponent<PlayerEventItens>().isNearDoor = false;
            OpenDoors();
        }
        else if (IsLocked)
        {
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            if (inventory.GetComponent<Inventory>().HasItemNamed(GetKeyType().ToString()))
            {
                IsLocked = false;
                inventory.GetComponent<Inventory>().DropItemByName(GetKeyType().ToString());
                Open(player, canvas);
            }
            else
            {
                audioSource.clip = LockedSound;
                audioSource.Play();
                canvas.GetComponentInChildren<TMP_Text>().text = "Locked";
            }
        }
    }
    
    public void ForceOpen(GameObject player, Canvas canvas){
        audioSource.clip = UnlockedOpenSound;
        audioSource.Play();
        canvas.enabled = false;
        player.GetComponent<PlayerEventItens>().isNearDoor = false;
        OpenDoors();
    }
    
    public void Update()
    {
    }
}
