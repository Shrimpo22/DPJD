using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenChest : MonoBehaviour
{
    public float rotationAmount = 90f;
    public Vector3 rotationAxis = Vector3.up;
    public float rotationSpeed = 1f;
    public float rayDistance = 5f;
    public TMP_Text textChest;
    private bool open = false;

    private Camera playerCamera;
    public AudioSource audioSource;

    private bool isRotating = false;

    void Start()
    {
        playerCamera = Camera.main;
        textClear();
    }

    public void openChest(){
        if(!open && !isRotating){
        textActivate();
         if (Input.GetKeyDown(KeyCode.E)){
            textClear();
            StartCoroutine(RotateObject());
            this.GetComponent<Collider>().enabled = false;
         }
        }
    }

    public void openChestByLock(){
        StartCoroutine(RotateObject());
        this.GetComponent<Collider>().enabled = false;
    }

    public void textActivate(){
      textChest.text  = "(E) Open Chest";
      textChest.gameObject.SetActive(true);  
    }
    public void textClear(){
      textChest.gameObject.SetActive(false);  
    }
    private void OnTriggerExit(Collider other){
        textClear();
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
        this.GetComponent<BoxCollider>().enabled = false;
    }
}
