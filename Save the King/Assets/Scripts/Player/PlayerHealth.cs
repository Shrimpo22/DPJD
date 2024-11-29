using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    Animator animator;
    public bool isInvulnerable;
    public GameObject player;

    private EnemyManager enemyManager;

    void Start(){
        enemyManager = FindObjectOfType<EnemyManager>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount){

        if (!isInvulnerable) {
            currentHealth -= amount;
            if(currentHealth > 0){
                animator.Play("takingDamage",0,0f);
            }else if(currentHealth <= 0 ){
                GetComponent<PlayerMovement>().enabled = false;
                animator.Play("Death");
                //respawns after 3 seconds
                StartCoroutine(Respawn());
            }
        }
    }

    IEnumerator Respawn()
    {
        isInvulnerable = true; // Is Invulnerable to not conflict with taking damage animation
        yield return new WaitForSeconds(5);

        if (GameManager.instance != null && GameManager.instance.respawnPoint != null)
        {

            ResetPlayer();
            enemyManager.ResetEnemies();
            
            yield return new WaitForSeconds(2); // 2 Seconds of invulnerability
            isInvulnerable = false;
        }
    }

    void ResetPlayer() {

        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false; // Disable temporarily to prevent conflicts

        animator.Play("Movement"); // Plays normal animations
        player.transform.position = GameManager.instance.respawnPoint.position; // Moves player to respawn point position
        player.transform.rotation = Quaternion.Euler(0, 90, 0); // Set the player's rotation

        controller.enabled = true;  // Re-enable the controller
        
        GetComponent<PlayerMovement>().enabled = true;
        currentHealth = maxHealth; // Resets HealthBar
    }

    // Update is called once per frame
    void Update()
    {
    }
}
