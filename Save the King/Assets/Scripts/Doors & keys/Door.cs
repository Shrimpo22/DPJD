using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour, IOpenable
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

    public void Open(float rotationWanted){
        RotationSpeed = rotationWanted;
        targetRotation = 90f * Angle;
        audioSource.clip = UnlockedOpenSound;
        audioSource.Play();
    }

    public int Angle = 1;
    public float RotationSpeed { get; set; } = 45f;
    public AudioClip LockedSound;
    public AudioClip UnlockedOpenSound;
    public Key.KeyType KeyType;

    // Additional private fields for internal use
    private float currentRotation = 0f;
    public float targetRotation = 0f;
    public AudioSource audioSource;

    // Start method
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    // Method to open the door
    public void Open(GameObject player, Canvas canvas)
    {
        if (!IsLocked && IsClosed)
        {
            targetRotation = 90f * Angle;
            audioSource.clip = UnlockedOpenSound;
            audioSource.Play();
            canvas.enabled = false;
            player.GetComponent<PlayerEventItens>().isNearDoor = false;
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
    public void ForceOpen(GameObject player, Canvas canvas)
    {
        targetRotation = 90f * Angle;
        audioSource.clip = UnlockedOpenSound;
        audioSource.Play();
        canvas.enabled = false;
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
