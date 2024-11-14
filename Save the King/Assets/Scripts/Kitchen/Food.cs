using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Food : MonoBehaviour
{
    private bool hasBeenGrabed = false; 
    public TMP_Text foodText;
   
    public GameObject foodPrefab; 

    private int healingAmount;
    void Start(){
        
        if (foodText == null) {
          foodText = GetComponentInChildren<TMP_Text>(); 
        }

        foodText.color = Color.white;
    
        textClear();
        SetHealingAmount();
    }
    
    public void textActivate(){
      foodText.text  = "(E) Eat Food";
      Debug.Log("AAAAAAA");
      foodText.gameObject.SetActive(true);  
    }

    public void textClear(){
      foodText.gameObject.SetActive(false);  
    }


    private void SetHealingAmount() {
        
        string prefabName = foodPrefab.name.ToLower();

        if (prefabName.Contains("cheeseblock") || prefabName.Contains("bread round")) {
            healingAmount = 10;
        }
        else if (prefabName.Contains("apple pie") || prefabName.Contains("cheesechunk")) {
            healingAmount = 20;
        }
        else {
            healingAmount = 10; 
        }
    }
    public void grabItem(GameObject player) {
            if(!hasBeenGrabed){
              float current = player.GetComponent<PlayerHealth>().currentHealth;
              float max = player.GetComponent<PlayerHealth>().maxHealth;
              if(current != max){
                  if(current+healingAmount> max){
                    player.GetComponent<PlayerHealth>().currentHealth = max; 
                  }else{
                    player.GetComponent<PlayerHealth>().currentHealth+=healingAmount;
                  }
                  hasBeenGrabed = true;
                  Debug.Log("Adicionei");
                  Destroy(this.gameObject);
              }
            }
          
   
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }
}
