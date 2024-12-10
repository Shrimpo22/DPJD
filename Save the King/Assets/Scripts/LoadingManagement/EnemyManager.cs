using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public class EnemyData
    {
        public GameObject enemyObject;      // Prefab reference
        public Vector3 initialPosition;     // Initial position of the enemy
        public Quaternion initialRotation;  // Initial rotation
        public NavMeshAgent navMeshAgent;   // Reference to the NavMeshAgent
    }

    public List<EnemyData> enemies = new();

    // Reference to the parent object containing all enemies
    public Transform enemiesParent;

    void Start()
    {
        // Find all enemies within the enemies parent and save their initial state
        foreach (Transform enemy in enemiesParent)
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

            EnemyData data = new()
            {
                enemyObject = enemy.gameObject,
                initialPosition = enemy.position,    // Save the initial position
                initialRotation = enemy.rotation,    // Save the initial rotation
                navMeshAgent = agent
            };
            enemies.Add(data);
        }
    }

    // Reset all enemies to their initial state
    public void ResetEnemies()
    {
        foreach (EnemyData data in enemies)
        {
            Debug.Log("Resetting " + data.enemyObject.name);
            // Reset the position and rotation of each enemy
            data.navMeshAgent.Warp(data.initialPosition);
            data.enemyObject.GetComponent<Animator>().ResetTrigger("Attack");
            data.enemyObject.GetComponent<Animator>().ResetTrigger("Throw");
            data.enemyObject.transform.rotation = data.initialRotation;

            // Reset any other components (e.g., AI state, health, etc.)
            data.enemyObject.GetComponent<AiAgent>().Reset();
            if (data.enemyObject.GetComponent<AudioSource>() != null)
                data.enemyObject.GetComponent<AudioSource>().Stop();
        }
    }
}
