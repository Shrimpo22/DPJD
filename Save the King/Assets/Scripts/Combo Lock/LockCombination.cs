using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class LockCombination : MonoBehaviour
{
    [SerializeField] private string combinationCorrect;
    [SerializeField] LockDial[] allIndexes = new LockDial[5];
    public TMP_Text textEvent;
    GameObject player;
    public Camera myCamera;
    private Camera mainCamera;
    public bool isLooking = true;
    [SerializeField] Transform shackle; 

    void Awake() {
       player = GameObject.FindGameObjectWithTag("Player");
       textEvent.gameObject.SetActive(false);
       createCorrectCombination();
       mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
       shackle.localPosition = new Vector3(0f,2f,1.45f);
        
    }  

    public void textActivate(){
      textEvent.text  = "(E) Interact";
      textEvent.gameObject.SetActive(true);  
    }

    public void textClear(){
      textEvent.gameObject.SetActive(false);  
    }
 
    private void createCorrectCombination()
    {
        string[] combinationNew = new string[5];
        Random rnd = new Random();
        for(int i=0; i<5;i++){
            if(i==0) { 
                combinationNew[i] = rnd.Next(1,8).ToString();
            }else{
            combinationNew[i] = rnd.Next(1,8).ToString();
            }
        }
        combinationCorrect = string.Join("", combinationNew);
       
    }

    public void seeLock() {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        mainCamera.tag="Untagged";
        myCamera.tag="MainCamera";
        player.SetActive(false);
        isLooking = true;
        mainCamera.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(true);
        textEvent.text  = "(ESC) Exit";
        for(int i =0; i<5;i++){allIndexes[i].canMove = true;}
        isLooking=true;
    }

    void Update()
    {
        if(verifyCombination()){
            //Add sound
            StartCoroutine(openLock());  
            textClear();
            player.SetActive(true);
            mainCamera.tag = "MainCamera";
            myCamera.tag="Untagged";
            mainCamera.gameObject.SetActive(true);
            myCamera.gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = true;
             Destroy(this.gameObject);
            
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
            player.SetActive(true);
            textClear();
            mainCamera.tag = "MainCamera";
            myCamera.tag="Untagged";
            mainCamera.gameObject.SetActive(true);
            myCamera.gameObject.SetActive(false);
            for(int i =0; i<5;i++){allIndexes[i].canMove = false;} 
            gameObject.GetComponent<BoxCollider>().enabled = true;
            isLooking = false;
           
           
        }
    }

    IEnumerator openLock()
    {
        Vector3 startPosition = shackle.localPosition;
        
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            
            shackle.localPosition = Vector3.Lerp(startPosition, new Vector3(0f, 3f, 1.45f), elapsedTime / 1f);

         
            elapsedTime += Time.deltaTime;

           
            yield return null;
        }

        shackle.localPosition =  new Vector3(0f, 3f, 1.45f);
       
    }

    bool verifyCombination(){
        for(int i =0; i<5;i++){
            if(allIndexes[i].getNr().ToString() != combinationCorrect[i].ToString())return false;
        }
        for(int i =0; i<5;i++){allIndexes[i].canMove = false;}

        return true;
       
        
    }
}
