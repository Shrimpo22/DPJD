using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenChest : MonoBehaviour, IOpenable
{
    public bool isLocked = false;
    public float rotationAmount = 90f;
    public Vector3 rotationAxis = Vector3.up;
    public float rotationSpeed = 1f;
    public float rayDistance = 5f;
    private bool open = false;

    public AudioClip LockedSound;
    public AudioClip UnlockedOpenSound;
    public Key.KeyType KeyType;

    public AudioSource audioSource;

    public bool isRotating = false;

    public bool IsClosed 
    { 
        get { return open; } 
        set { open = value; }
    }

    public void ForceOpen(GameObject player, Canvas canvas){
        if(canvas != null){
            canvas.gameObject.SetActive(false);
        }
        StartCoroutine(RotateObject());
        this.GetComponent<Collider>().enabled = false;
    }

    IEnumerator RotateObject()
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(rotationAxis * rotationAmount);

        float timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed);
            timeElapsed += Time.deltaTime * rotationSpeed;

            yield return null;
        }

        transform.rotation = endRotation;

        isRotating = false;

        open = true;
        //this.GetComponent<BoxCollider>().enabled = false;
    }

    public void Open(GameObject player, Canvas canvas)
    {
        if(!isLocked && !open && !isRotating){
            canvas.gameObject.SetActive(false);
            StartCoroutine(RotateObject());
            audioSource.clip = UnlockedOpenSound;
            audioSource.Play();
            this.GetComponent<Collider>().enabled = false;
        }else if(isLocked){
             GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            if (inventory.GetComponent<Inventory>().HasItemNamed(KeyType.ToString()))
            {
                isLocked = false;
                inventory.GetComponent<Inventory>().DropItemByName(KeyType.ToString());
                Open(player, canvas);
            }
            else
            {
                audioSource.clip = LockedSound;
                audioSource.Play();
                IsClosed = true;
                canvas.GetComponentInChildren<TMP_Text>().text = "Locked";
            }
        }
    }

    public void Update()
    {
    }
}