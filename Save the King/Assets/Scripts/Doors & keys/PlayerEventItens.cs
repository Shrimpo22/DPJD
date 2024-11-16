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
    public List<GameObject> Objects{
        get{
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
    public int rayCount = 2; 
    public float rayDistance = 3f; 
    public float spacing = 1f; 
    public List<Key.KeyType> listOfKeys;
    public List<GameObject> weapons;
    private Key key;
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

    public void AddKey(Key.KeyType keytype){
        listOfKeys.Add(keytype);
    }

    public void RemoveKey(Key.KeyType keytype){
        listOfKeys.Remove(keytype);
    }
    
    // void HandleInteraction()  {
    //     if(isNearDoor){ 
    //         door.OpenDoor(this.gameObject);
    //     }else{
    //     // Set the base position for raycasting
    //     Vector3 basePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
    //     for (int x = -rayCount/2; x <= rayCount/2; x++)
    //     {
    //         for (int z = -rayCount; z <= rayCount; z++)
    //         {   
    //             Vector3 rayOrigin = basePosition + new Vector3(x * spacing, 3f, z * spacing);

                
    //             RaycastHit hit;
    //             Debug.DrawRay(rayOrigin, Vector3.down * rayDistance, Color.magenta);
    //             if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance))
    //             {
    //                 //Debug.Log("Hit: " + hit.collider.tag); // Log what was hit
    //                 Debug.Log("Hit " + hit.collider.tag + " " + hit.collider.gameObject.name);
    //                 HandleHit(hit.collider);
                    
    //             }
    //         }
    //     }
    //     }
    // }
    void HandleInteraction(){
        if(closestInteractable != null)
            closestInteractable.Interact(inventory, this);
    }
    void Update()
    {
        scanTimer -= Time.deltaTime;
        if(scanTimer < 0 ){
            scanTimer += scanInterval;
            Scan();
        }
    }

    public void Scan(){
        playerCamera = Camera.main;
        if (playerCamera == null)
            Debug.LogError("No camera tagged as MainCamera found in the scene!");

        Interactable interactableAux = GetClosestInteractable();
        if(closestInteractable != interactableAux){
            //Debug.Log("Closest Interactable Changed " + closestInteractable?.gameObject.name + " to " + interactableAux?.gameObject.name );
            if(closestInteractable != null) closestInteractable.ExitRange();
                closestInteractable = interactableAux;
            if(closestInteractable != null) closestInteractable.EnterRange();
            else closestDistance = float.MaxValue;
        }
        
    }

    public void ClearClosestInteractable(){
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

                if (distance < closestDistance || Math.Abs(distance - closestDistance) < 0.5f){
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
        if(obj == null) return false;
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

    private void HandleHit(Collider hit){

        //Debug.Log("Hit " + hit.tag);

            if (hit.tag == "Key"){
                key = hit.gameObject.GetComponent<Key>();
                key.textActivate();
                key.grabKey(this.gameObject);
            }else if(hit.tag== "Chest") {
                hit.gameObject.GetComponent<OpenChest>().openChest();
            }else if(hit.tag == "Lock"  &&hit.gameObject.GetComponent<LockCombination>().isLooking==false){
               hit.gameObject.GetComponent<LockCombination>().textActivate();
               hit.gameObject.GetComponent<LockCombination>().seeObject();
            }else if(hit.tag == "MirrorPiece"){
                MirrorPiece mirrorPiece = hit.gameObject.GetComponent<MirrorPiece>();
                mirrorPiece.textActivate();
                mirrorPiece.grabMirrorPiece();
            }else if(hit.tag == "Candelabra" && hit.gameObject.GetComponent<CandelabraCam>().active && hit.gameObject.GetComponent<CandelabraCam>().isLooking == false){
                hit.gameObject.GetComponent<CandelabraCam>().textActivate();
                hit.gameObject.GetComponent<CandelabraCam>().seeObject();
            }else if(hit.tag == "MirrorTable" && hit.gameObject.GetComponent<MirrorTableCam>().isLooking == false){
                hit.gameObject.GetComponent<MirrorTableCam>().textActivate();
                hit.gameObject.GetComponent<MirrorTableCam>().seeObject();
            }else if (hit.tag == "Painting" && hit.gameObject.GetComponent<PaintingCam>().isLooking==false){
                hit.gameObject.GetComponent<PaintingCam>().textActivate();
                hit.gameObject.GetComponent<PaintingCam>().seeObject();
            }else if(hit.tag == "Map"  &&hit.gameObject.GetComponent<Map>().isLooking==false){
               hit.gameObject.GetComponent<Map>().textActivate();
               hit.gameObject.GetComponent<Map>().seeObject();
            }
            else if(hit.tag == "MapPiece"){
                hit.gameObject.GetComponent<MapPieceGrab>().textActivate();
                hit.gameObject.GetComponent<MapPieceGrab>().grabItem(this.gameObject);
            }
            else if (hit.tag == "Food"){
                hit.gameObject.GetComponent<Food>().textActivate();
                hit.gameObject.GetComponent<Food>().grabItem(this.gameObject);
            }
            else if (hit.tag == "Cookitems"){
                hit.gameObject.GetComponent<CookItems>().textActivate();
                hit.gameObject.GetComponent<CookItems>().grabItem(this.gameObject);
            }
            else if (hit.tag == "furnalha" && hit.gameObject.GetComponent<CozinharScript>().isLooking==false){
                hit.gameObject.GetComponent<CozinharScript>().textActivate();
                hit.gameObject.GetComponent<CozinharScript>().seeObject();
            }
            else if (hit.tag == "Weapon In Ground")
            {
            }else if (gameObject.GetComponent<PlayerMovement>().isCrouching)
            {
                if(hit.tag == "Target")
                {
                    Debug.Log("AAAAAAA");
                    gameObject.GetComponent<PlayerMovement>().stealthAttack();
                }
            }
    }

    public void DeactivateSword(GameObject inventory)
    {
        inventory.GetComponent<Inventory>().AddItem(swordOn.name,1);
        string nameOfObj = swordOn.name;
        GameObject.Find(nameOfObj).SetActive(false);
        swordOn = null;
    }

    public void InstantiateSword(string str)
    {
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        if(hasSwordOn)
        {
            DeactivateSword(inventory);
        }

        GameObject prefab = Resources.Load<GameObject>("Weapons/"+str); // Instantiate the matching GameObject
        
        if(prefab == null){
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

    public void OnDrawGizmos(){
        if(drawRange){
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, interactRange);

            Gizmos.color = Color.green;
            foreach (var obj in objects){
                if(obj == null) continue;
                Gizmos.color = IsInView(obj)? Color.green: Color.red;
                Renderer objRenderer = obj.GetComponent<Renderer>();
                if (objRenderer != null){
                    Gizmos.DrawWireCube(objRenderer.bounds.center, objRenderer.bounds.size + new Vector3(0.2f,0.2f,0.2f)); // Draw wireframe based on bounds
                }
            }
        }
    }
}




    
    

    

    


