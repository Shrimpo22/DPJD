using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    Animator animator;
    public bool isInvulnerable;

    void Start(){
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount){
        currentHealth -= amount;
        if (!isInvulnerable) {
            if(currentHealth > 0){
                animator.Play("takingDamage",0,0f);
            }else if(currentHealth <= 0){
                GetComponent<PlayerMovement>().enabled = false;
                animator.Play("Death");
                //respawns after 3 seconds
                StartCoroutine(Respawn());
            }
        }
    }

     IEnumerator Respawn()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(5);

        EnemyManager enemyManager = GameManager.instance.currentEnemyManager;
        if (GameManager.instance != null && GameManager.instance.respawnPoint != null)
        {
            ResetPlayer();
            if (enemyManager != null)
            {
                enemyManager.ResetEnemies();
                Debug.Log("reseting enemies...");
            }
            yield return new WaitForSeconds(2);
            isInvulnerable = false;
        }
        Debug.Log("inside respawn...");
    }

     void ResetPlayer() {
        GameObject player = gameObject;
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false; // Disable temporarily to prevent conflicts

        animator.Play("Movement"); // Plays normal animations
        player.transform.position = GameManager.instance.respawnPoint.position; // Moves player to respawn point position
        player.transform.rotation = Quaternion.Euler(0, 90, 0); // Set the player's rotation

        controller.enabled = true;  // Re-enable the controller
        
        GetComponent<PlayerMovement>().enabled = true;
        currentHealth = maxHealth; // Resets HealthBar
    }

}
