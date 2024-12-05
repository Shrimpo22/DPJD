using UnityEngine;


public class EnemyDataState
{
    public string enemyName;
    public Vector3 position;
    public Quaternion rotation;
    public bool isDead;   // Store if the enemy is dead or not (if applicable)
}

public class GameObjectState
{
    public string objectName; // The name of the GameObject (could also be tag or another identifier)
    public bool isActive;     // Whether the GameObject is active or not
    public Vector3 position;
}
