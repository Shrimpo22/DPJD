using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


public class LockInteractable : CamInteractable
{
    [SerializeField] private string combinationCorrect;
    [SerializeField] LockDial[] allIndexes = new LockDial[5];
    [SerializeField] Transform shackle; 

    public GameObject chestLocked;

    public override void Awake(){
        base.Awake();
        CreateCorrectCombination();
        shackle.localPosition = new Vector3(0f,2f,1.45f);
    }

    private void CreateCorrectCombination()
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
        GameObject.FindGameObjectWithTag("Map").GetComponent<MapInteractable>().getTextFinalAnswer(combinationCorrect);
    }

    IEnumerator OpenLock()
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

    bool VerifyCombination(){
        for(int i =0; i<5;i++){
            if(allIndexes[i].getNr().ToString() != combinationCorrect[i].ToString())return false;
        }
        for(int i =0; i<5;i++){allIndexes[i].canMove = false;}

        return true;
    }

    public override void Start(){
        interactCanvas.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(false);
        
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        isLooking = false;
    }

    public override void Interact(Inventory inv, PlayerEventItens playerItems)
    {
        base.Interact(inv, playerItems);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        for(int i =0; i<5;i++){allIndexes[i].canMove = true;}
    }

    public override void ExitCam()
    {
        base.ExitCam();
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    void Update()
    {
        if(VerifyCombination()){
            //Add sound
            StartCoroutine(OpenLock());  
            ExitCam();
            chestLocked.GetComponent<OpenableInteractable>().ForceOpen();
            Destroy(this.gameObject);
        }
    }
}
