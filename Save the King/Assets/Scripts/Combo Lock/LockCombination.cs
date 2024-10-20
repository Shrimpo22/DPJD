using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class LockCombination : MonoBehaviour
{
    [SerializeField] private string combinationCorrect;
    [SerializeField] LockDial[] allIndexes = new LockDial[5];

    [SerializeField] Transform shackle; 

    void Awake() {
        createCorrectCombination();
        shackle.localPosition = new Vector3(0f,2f,1.45f);
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

    // Update is called once per frame
    void Update()
    {
        if(verifyCombination()){
            //Add sound
            StartCoroutine(openLock());   
        }
    }

    IEnumerator openLock()
    {
        Vector3 startPosition = shackle.localPosition;
        
        float elapsedTime = 0f;

        // While loop to move the locker over time
        while (elapsedTime < 1f)
        {
            // Linearly interpolate the localPosition
            shackle.localPosition = Vector3.Lerp(startPosition, new Vector3(0f, 3f, 1.45f), elapsedTime / 1f);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the locker ends at the exact end position
        shackle.localPosition =  new Vector3(0f, 3f, 1.45f);
        this.gameObject.SetActive(false);
    }

    bool verifyCombination(){
        for(int i =0; i<5;i++){
            if(allIndexes[i].getNr().ToString() != combinationCorrect[i].ToString())return false;
        }
        for(int i =0; i<5;i++){allIndexes[i].canMove = false;}
        return true;
       
        
    }
}
