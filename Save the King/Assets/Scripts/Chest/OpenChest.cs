using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public float rotationAmount = 90f;
    public Vector3 rotationAxis = Vector3.up;
    public float rotationSpeed = 1f;
    public float rayDistance = 5f;

    private bool open = false;

    private Camera playerCamera;
    public AudioSource audioSource;

    private bool isRotating = false;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.transform == transform && open == false)
                {
                    StartCoroutine(RotateObject());
                }
            }
        }
    }

    IEnumerator RotateObject()
    {
        isRotating = true;

        if (audioSource != null)
        {
            audioSource.Play();
        }

        Transform parentTransform = transform.parent;

        Quaternion startRotation = parentTransform.rotation;
        Quaternion endRotation = parentTransform.rotation * Quaternion.Euler(rotationAxis * rotationAmount);

        float timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            parentTransform.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed);
            timeElapsed += Time.deltaTime * rotationSpeed;

            yield return null;
        }

        parentTransform.rotation = endRotation;

        isRotating = false;

        open = true;
    }
}
