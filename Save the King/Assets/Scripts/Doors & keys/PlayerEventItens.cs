using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class PlayerEventItens : MonoBehaviour
{
    Collider[] colliders = new Collider[50];
    int count;
    public List<GameObject> Objects
    {
        get
        {
            objects.RemoveAll(obj => !obj);
            return objects;
        }
    }
    private List<GameObject> objects = new List<GameObject>();
    public LayerMask layersForRaycast;

    [SerializeField] private Interactable closestInteractable;
    public float closestDistance = float.MaxValue;



    public int scanFrequency = 10;
    float scanTimer;
    float scanInterval;
    public bool drawRange = false;
    public bool isNearDoor = false;

    private PlayerControls controls;

    public bool hasSwordOn = false;

    public GameObject swordOn = null;

    public float interactRange = 5f; // The range within which the player can interact
    private Camera playerCamera; // Reference to the player's camera

    public LayerMask interactableLayer;
    private Inventory inventory;



    void Awake()
    {
        controls = InputManager.inputActions;
        controls.Gameplay.Interaction.performed += ctx => HandleInteraction();
        scanInterval = 1.0f / scanFrequency;
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void HandleInteraction()
    {
        if (closestInteractable != null)
        {
            if (inventory.isZoomedIn == true && closestInteractable is CamInteractable aux)
            {
                Debug.Log("Alo");
                aux.ExitCam();
            }
            else
            {
                closestInteractable.Interact(inventory, this);
            }
        }
    }

    void Update()
    {
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }
    }

    public void Scan()
    {
        if (gameObject.GetComponent<PlayerMovement>().isTrapped)
        {
            ClearClosestInteractable();
            return;
        }
        playerCamera = Camera.main;
        if (playerCamera == null)
            Debug.LogError("No camera tagged as MainCamera found in the scene!");

        Interactable interactableAux = GetClosestInteractable();
        if (closestInteractable != interactableAux)
        {
            //Debug.Log("Closest Interactable Changed " + closestInteractable?.gameObject.name + " to " + interactableAux?.gameObject.name );
            if (closestInteractable != null) closestInteractable.ExitRange();
            closestInteractable = interactableAux;
            if (closestInteractable != null) closestInteractable.EnterRange();
            else closestDistance = float.MaxValue;
        }

    }

    public void ClearClosestInteractable()
    {
        closestInteractable = null;
        closestDistance = float.MaxValue;
    }

    Interactable GetClosestInteractable()
    {
        Interactable interactable1 = null;

        count = Physics.OverlapSphereNonAlloc(transform.position, interactRange, colliders, interactableLayer);
        objects.Clear();
        // Debug.Log("Objects in range: " + count);
        for (int i = 0; i < count; i++)
        {

            Collider collider = colliders[i];
            GameObject obj = collider.gameObject;
            // Debug.Log("Collided with" + obj.name + " and obj " + (IsInView(obj)?"in":"not in") + "view" );

            objects.Add(obj);

            Interactable interactable = obj.GetComponent<Interactable>();
            // Debug.Log("Wha? " + IsInView(obj));
            if (interactable != null && IsInView(obj))
            {
                // Debug.Log("Wha?2 ");
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                // Debug.Log("Wha?2.5 d" + distance + " cd" + closestDistance + " Abs" +Math.Abs(distance - closestDistance)); 

                if (distance < closestDistance || Math.Abs(distance - closestDistance) < 0.5f)
                {
                    // Debug.Log("Wha?3 "); 
                    closestDistance = distance;
                    interactable1 = interactable;
                }
            }
        }


        return interactable1;
    }

    bool IsInView(GameObject obj)
    {
        if (obj == null) return false;
        Vector3 screenPoint = playerCamera.WorldToViewportPoint(obj.transform.position);

        bool isInView = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        // Debug.Log("Is it? " + isInView);
        if (isInView)
        {
            // Debug.Log("Here1");
            Vector3 directionToTarget = obj.GetComponent<Collider>().bounds.center - playerCamera.transform.position;
            // Debug.Log("Here1.5" + Physics.Raycast(playerCamera.transform.position, directionToTarget, out RaycastHit hit2, 100, layersForRaycast));
            // Debug.DrawRay(playerCamera.transform.position, directionToTarget, Color.blue);
            if (Physics.Raycast(playerCamera.transform.position, directionToTarget, out RaycastHit hit, 100, layersForRaycast))
            {
                // Debug.Log("Here2");
                // Debug.DrawRay(playerCamera.transform.position, directionToTarget, Color.red);
                return hit.collider.gameObject == obj;
            }
        }

        return false;
    }

    public void DeactivateSword(GameObject inventory)
    {
        inventory.GetComponent<Inventory>().AddItem(swordOn.name, 1);
        string nameOfObj = swordOn.name;
        GameObject.Find(nameOfObj).SetActive(false);
        swordOn = null;
    }

    public void InstantiateSword(string str)
    {
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        if (hasSwordOn)
        {
            DeactivateSword(inventory);
        }

        GameObject prefab = Resources.Load<GameObject>("Weapons/" + str); // Instantiate the matching GameObject

        if (prefab == null)
        {
            Debug.LogError("Prefab " + str + " not found in Resources folder");
            return;
        }
        GameObject obj = Instantiate(prefab);
        obj.GetComponent<PlayerAttack>().EquipSword();
        string originalName = obj.name;
        obj.name = originalName.Replace("(Clone)", "").Trim();
        hasSwordOn = true;
        swordOn = obj;

        inventory.GetComponent<Inventory>().DropItemByName(str);

        return;
    }

    public void OnDrawGizmos()
    {
        if (drawRange)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, interactRange);

            Gizmos.color = Color.green;
            foreach (var obj in objects)
            {
                if (obj == null) continue;
                Gizmos.color = IsInView(obj) ? Color.green : Color.red;
                Renderer objRenderer = obj.GetComponent<Renderer>();
                if (objRenderer != null)
                {
                    Gizmos.DrawWireCube(objRenderer.bounds.center, objRenderer.bounds.size + new Vector3(0.2f, 0.2f, 0.2f)); // Draw wireframe based on bounds
                }
            }
        }
    }
}












