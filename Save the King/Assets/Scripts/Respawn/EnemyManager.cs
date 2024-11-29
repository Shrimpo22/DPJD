using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyData
    {
        public GameObject enemyPrefab; // Prefab original do inimigo
        public Transform initialPosition; // Posição inicial do inimigo
        public Quaternion initialRotation; // Rotação inicial
        public NavMeshAgent navMeshAgent;  // Referência ao NavMeshAgent
    }

    public List<EnemyData> enemies = new();

    // Referência para o objeto vazio "Enemies"
    public Transform enemiesParent;

    void Start()
    {
        // Encontrar todos os inimigos dentro do gameobject com inimigos
        foreach (Transform enemy in enemiesParent)
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

            EnemyData data = new()
            {
                enemyPrefab = enemy.gameObject,  // Prefab do inimigo
                initialPosition = enemy.transform,  // Guardar a posição inicial
                initialRotation = enemy.transform.rotation,  // Guardar a rotação inicial
                navMeshAgent = agent               // Guardar a referência ao NavMeshAgent
            };
            Debug.Log(data);
            enemies.Add(data);
        }
    }

    // Reseta todos os inimigos
    public void ResetEnemies()
    {
        
        foreach (EnemyData data in enemies)
        {
            Debug.Log("Resetting positions...");
            // Resetar a posição e rotação dos inimigos existentes
            data.navMeshAgent.Warp(data.initialPosition.position);
            data.enemyPrefab.transform.rotation = data.initialRotation;

            // Reset Animation, State, HealthBar and other components in each Enemy
            data.enemyPrefab.GetComponent<AiAgent>().Reset();
        }
            
    }
}

