using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    Animator animator;
    public bool isInvulnerable = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (!isInvulnerable)
        {
            currentHealth -= amount;
            if (currentHealth > 0)
            {
                animator.Play("takingDamage", 0, 0f);
            }
            else if (currentHealth <= 0)
            {
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<CharacterController>().enabled = false;
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
            GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>().SetGameState(GameManager.instance.respawnPoint.GetComponent<RespawnPoint>().musicToChangeTo);
            ResetPlayer();
            if (enemyManager != null)
            {
                enemyManager.ResetEnemies();
                Debug.Log("reseting enemies...");
            }
            // Reset room objects (e.g., doors, switches) to their initial state
            ResetRoomState();

            yield return new WaitForSeconds(2);
            isInvulnerable = false;
        }
    }

    private void ResetRoomState()
    {
        // Reset each room object to its initial state
        foreach (var obj in GameManager.instance.respawnPoint.GetComponent<RespawnPoint>().roomObjects)
        {
            if (obj != null)
            {
                RoomObjectState roomObjectState = obj.GetComponent<RoomObjectState>();
                if (roomObjectState != null)
                {
                    Debug.Log("resetting objects");
                    roomObjectState.ResetToInitialState(); // Reset the object
                }
            }
        }

        List<GameObject> pickablesToSave = GameManager.instance.respawnPoint.GetComponent<RespawnPoint>().pickablesToSpawn;
        List<GameObject> pickablesToSpawn = GameManager.instance.respawnPoint.GetComponent<RespawnPoint>().pickablesToSpawn2;
        GameManager.instance.RespawnNewPickables(pickablesToSpawn, pickablesToSave);
        ResetInventory(pickablesToSave);
    }

    private void ResetInventory(List<GameObject> objectsToRemove)
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        foreach (var obj in objectsToRemove)
        {
            PickupInteractable pickable = obj.GetComponentInChildren<PickupInteractable>();
            if (pickable != null && inventory.HasItemNamed(pickable.itemToGive.ToString()))
            {
                Debug.Log("removing " + pickable.itemToGive.ToString() + " from inventory.");
                inventory.DropItemByName(pickable.itemToGive.ToString());
            }
        }
    }
    public GameObject GetItemByName(List<GameObject> objects, string targetName)
    {
        foreach (GameObject obj in objects)
        {
            if (obj.name == targetName)
            {
                return obj; // Return the object if the name matches
            }
        }

        return null; // Return null if no object with the target name is found
    }

    void ResetPlayer()
    {
        bool canBringSword = GameManager.instance.respawnPoint.GetComponent<RespawnPoint>().canBringSword;

        GameObject player = gameObject;
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false; // Disable temporarily to prevent conflicts
        GetComponent<PlayerMovement>().NotDetected();
        GetComponent<PlayerMovement>().ResetInteraction();
        GetComponent<PlayerMovement>().isTrapped = false;
        animator.Play("Movement"); // Plays normal animations
        player.transform.position = GameManager.instance.respawnPoint.position; // Moves player to respawn point position
        player.transform.rotation = Quaternion.Euler(0, 90, 0); // Set the player's rotation

        controller.enabled = true;  // Re-enable the controller

        GetComponent<PlayerMovement>().enabled = true;
        currentHealth = maxHealth; // Resets HealthBar


        GameObject weaponLocation = GameManager.instance.weaponLocation;
        if (weaponLocation != null && !canBringSword)
        {

            GetComponent<PlayerMovement>().isSwordEquipped = false;
            GetComponent<PlayerEventItens>().hasSwordOn = false;
            if (weaponLocation.transform.childCount > 0)
            {
                GameObject sword = weaponLocation.transform.GetChild(0).gameObject;
                if (sword != null)
                {
                    Destroy(sword);
                    GetComponent<PlayerEventItens>().swordOn = null;
                }
            }
        }
    }
}
