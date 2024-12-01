using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState
{
    public Transform PlayerNextSceneSpawnPoint;

    public Transform playerLastPosition;

    public Dictionary<string, bool> doorStates; // Save door open/closed states
    
    public List<GameObject> enemyStates;

    public GameObject playerHUD;
    public GameObject playerGO;

    public Inventory inv;
}

