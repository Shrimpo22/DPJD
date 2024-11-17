using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class PlayerEventItens : MonoBehaviour
{   
    public bool isNearDoor = false;
    public int rayCount = 2; 
    public float rayDistance = 3f; 
    public float spacing = 1f; 
    public List<Key.KeyType> listOfKeys;
    private IDoor door;
    public List<GameObject> weapons;
    private Key key;
    private PlayerControls controls;

    public bool hasSwordOn = false;

    public GameObject swordOn = null;


    void Awake()
    {
        controls = InputManager.inputActions;
        controls.Gameplay.Interaction.performed += ctx => HandleInteraction();
    }

    void OnEnable(){
        controls.Gameplay.Enable();
    }

    void OnDisable(){
        controls.Gameplay.Disable();
    }
    public void AddKey(Key.KeyType keytype){
        listOfKeys.Add(keytype);
    }

    public void RemoveKey(Key.KeyType keytype){
        listOfKeys.Remove(keytype);
    }

     private void OnTriggerEnter(Collider other) {
        if(other.tag == "Door"){ 
            Debug.Log("Collided Door");
            door = other.gameObject.GetComponent<Door>();
            door = door != null ? door : other.gameObject.GetComponent<DoubleDoor>();
                if(door.IsClosed){
                    door.TextActivate();
                    isNearDoor = true;
                }       
        }
    }
    void HandleInteraction()  {
        if(isNearDoor){ 
            door.OpenDoor(this.gameObject);
        }else{
        // Set the base position for raycasting
        Vector3 basePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        for (int x = -rayCount/2; x <= rayCount/2; x++)
        {
            for (int z = -rayCount; z <= rayCount; z++)
            {   
                Vector3 rayOrigin = basePosition + new Vector3(x * spacing, 3f, z * spacing);

                
                RaycastHit hit;
                Debug.DrawRay(rayOrigin, Vector3.down * rayDistance, Color.magenta);
                if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance))
                {
                    //Debug.Log("Hit: " + hit.collider.tag); // Log what was hit
                    Debug.Log("Hit " + hit.collider.tag + " " + hit.collider.gameObject.name);
                    HandleHit(hit.collider);
                    
                }
            }
        }
        }
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
            else if(hit.tag == "Torch"){
                hit.gameObject.GetComponent<Torches>().textActivate();
                hit.gameObject.GetComponent<Torches>().grabTorch();
            }
            else if(hit.tag == "Chalice"){
                hit.gameObject.GetComponent<ChaliceObj>().textActivate();
                hit.gameObject.GetComponent<ChaliceObj>().grabChalice();
            }else if(hit.tag == "Candle"){
                hit.gameObject.GetComponent<Candles>().textActivate();
                hit.gameObject.GetComponent<Candles>().grabCandle();
            }
            else if (hit.tag == "Statue" && hit.gameObject.GetComponent<Statues>().isLooking==false){
                hit.gameObject.GetComponent<Statues>().textActivate();
                hit.gameObject.GetComponent<Statues>().seeObject();
            }
            else if (hit.tag == "Organ" && hit.gameObject.GetComponent<Organ>().isLooking==false){
                hit.gameObject.GetComponent<Organ>().textActivate();
                hit.gameObject.GetComponent<Organ>().seeObject();
            }
            else if (hit.tag == "MusicSheet"){
                hit.gameObject.GetComponent<Music>().textActivate();
                hit.gameObject.GetComponent<Music>().grabMusicSheet();
            }
            else if (hit.tag == "CandleHolder" && hit.gameObject.GetComponent<CandleHolder>().isLooking==false){
                hit.gameObject.GetComponent<CandleHolder>().textActivate();
                hit.gameObject.GetComponent<CandleHolder>().seeObject();
            }
            //else if (hit.tag == "Book" && hit.gameObject.GetComponent<BookOpening>().isLooking==false){
            //    hit.gameObject.GetComponent<BookOpening>().textActivate();
            //    hit.gameObject.GetComponent<BookOpening>().seeObject();
            //}
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
                hit.gameObject.GetComponent<PlayerAttack>().textActivate();
                weapons.Add(hit.gameObject);
                hit.gameObject.GetComponent<PlayerAttack>().grabSword();
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
        foreach (GameObject obj in weapons)
        {
            if (obj.name == str) // Check if the name matches
            {
                Instantiate(obj); // Instantiate the matching GameObject
                Debug.Log("Instantiated object: " + name);
                obj.GetComponent<PlayerAttack>().EquipSword();
                hasSwordOn = true;
                swordOn = obj;

                inventory.GetComponent<Inventory>().DropItemByName(str);

                return;
            }
        }
    }
}




    
    

    

    


