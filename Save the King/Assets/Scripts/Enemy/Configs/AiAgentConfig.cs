using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float speed = 10;
    public float maxSightDistance = 10;
    public float angle = 30;
    public float height = 1;
    public Color meshColor = Color.red;
    public float stoppingDistance = 0;
    public float alertRate = 0.1f;
    public float powerAmount = 0.3f;
    public float maxHealth = 100;
    public float patrolInterval = 5f;
    public float patrolSpeed = 4f;

}
