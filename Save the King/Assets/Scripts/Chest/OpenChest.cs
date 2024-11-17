using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenChest : MonoBehaviour, IOpenable
{
    public float rotationAmount = 90f;
    public Vector3 rotationAxis = Vector3.up;
    public float rotationSpeed = 1f;
    public float rayDistance = 5f;
    private bool open = false;

    public AudioSource audioSource;

    private bool isRotating = false;

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

        if (audioSource != null)
        {
            audioSource.Play();
        }

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
        if(!open && !isRotating){
            canvas.gameObject.SetActive(false);
            StartCoroutine(RotateObject());
            this.GetComponent<Collider>().enabled = false;
        }
    }

    public void Update()
    {
    }
}
