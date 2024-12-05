using UnityEngine;


public class EnemyDataState
{
    public Vector3 position;          // Current position of the enemy
    public Quaternion rotation;       // Current rotation of the enemy
    public GameObject enemyObject;    // Reference to the actual enemy GameObject
}

public class GameObjectState
{
    public string objectName; // The name of the GameObject (could also be tag or another identifier)
    public bool isActive;     // Whether the GameObject is active or not
    public Vector3 position;
}
