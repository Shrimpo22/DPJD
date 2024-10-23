using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;

public class DetectionArrow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public GameObject detectionArrowPrefab;  // Prefab for the detection arrow UI
    public Transform arrowParent;  // The parent object to hold arrows (inside a Canvas)
    public float detectionDistance = 15f;  // Max distance to display arrows

     public float arrowDistanceFromCenter = 200f;  // How far from the center the arrow should be
    public Vector2 arrowScale = new Vector2(1.5f, 1.5f);  // Scale for the arrow to make it larger

    public Vector2 arrowOffset = new Vector2(0, -10);

    public float smoothingFactor = 5f;  // Factor to control smoothing of arrow movement

    public Vector3 normalizedDirection;    
    public Vector3 finalDir;    
    public RectTransform arrowRectTransform;


    // Dictionary to keep track of arrows associated with each detecting enemy
    private Dictionary<EnemyDetection, GameObject> activeArrows = new Dictionary<EnemyDetection, GameObject>();

    void OnEnable()
    {
        // Subscribe to detection events
        EnemyDetection.OnDetectPlayer += HandleEnemyDetectingPlayer;
        EnemyDetection.OnLosePlayer += HandleEnemyLostPlayer;
        EnemyDetection.OnChasePlayer += HandleEnemyChasePlayer;
        EnemyDetection.OnLook4Player += HandleEnemyLookForPlayer;
    }

    void OnDisable()
    {
        // Unsubscribe from detection events to prevent memory leaks
        EnemyDetection.OnDetectPlayer -= HandleEnemyDetectingPlayer;
        EnemyDetection.OnLosePlayer -= HandleEnemyLostPlayer;
    }

    void Update()
    {
        // Update each active arrow to point toward its corresponding enemy
        foreach (var entry in activeArrows)
        {
            EnemyDetection enemy = entry.Key;
            GameObject arrow = entry.Value;

            // Only update the arrow if the enemy is within detection distance
            float distanceToPlayer = Vector3.Distance(player.position, enemy.enemyCenter.position);
            if (distanceToPlayer <= detectionDistance)
            {
                arrow.SetActive(true);

                // Calculate direction from camera to the enemy
                Vector3 direction = (enemy.enemyCenter.position - Camera.main.transform.position).normalized;

                // Get the camera's forward direction (ignoring Y-axis to maintain horizontal direction)
                Vector3 cameraForward = Camera.main.transform.forward;
                Vector3 cameraRight = Camera.main.transform.right;
                cameraForward.y = 0; 
                cameraRight.y = 0; 

                Vector3 forwardDir = direction.x * cameraForward;
                Vector3 rightDir = direction.z * cameraRight;

                finalDir = forwardDir + rightDir;

                arrowRectTransform = arrow.GetComponent<RectTransform>();

                // Rotate the arrow to point towards the enemy
                float angle = Mathf.Atan2(finalDir.x, finalDir.z) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);

                // Smooth the rotation of the arrow
                arrowRectTransform.rotation = Quaternion.Slerp(arrowRectTransform.rotation, targetRotation, Time.deltaTime * smoothingFactor);

                Image img = arrow.GetComponentInChildren<Image>();
                img.fillAmount = enemy.agent.alertState;

                if(img.fillAmount < 1f){
                    img.color = Color.white;
                }
            }
            else
            {
                arrow.SetActive(false);  // Hide the arrow if the enemy is too far
            }
        }
    }

    // Event handler: Called when an enemy starts detecting the player
    private void HandleEnemyDetectingPlayer(EnemyDetection enemy)
    {
        if (!activeArrows.ContainsKey(enemy))
        {
            // Instantiate a new detection arrow for this enemy
            GameObject newArrow = Instantiate(detectionArrowPrefab, arrowParent);
            activeArrows.Add(enemy, newArrow);
        }
    }

    // Event handler: Called when an enemy stops detecting the player
    private void HandleEnemyLostPlayer(EnemyDetection enemy)
    {
        if (activeArrows.ContainsKey(enemy))
        {
            // Destroy the arrow associated with this enemy
            Destroy(activeArrows[enemy]);
            activeArrows.Remove(enemy);
        }
    }

    private void HandleEnemyChasePlayer(EnemyDetection enemy){
        if (activeArrows.ContainsKey(enemy))
        {
            Image img = activeArrows[enemy].GetComponentInChildren<Image>();
            img.color = Color.red;
        }
    }

    private void HandleEnemyLookForPlayer(EnemyDetection enemy){
        if (activeArrows.ContainsKey(enemy))
        {
            Image img = activeArrows[enemy].GetComponentInChildren<Image>();
            img.color = Color.yellow;
        }
    }

}
