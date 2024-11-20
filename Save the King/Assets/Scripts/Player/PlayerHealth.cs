using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    Animator animator;
    void Start(){
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount){
        currentHealth -= amount;

        if(currentHealth > 0){
            animator.Play("takingDamage",0,0f);
        }else if(currentHealth <= 0){
            GetComponent<PlayerMovement>().enabled = false;
            animator.Play("Death");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
