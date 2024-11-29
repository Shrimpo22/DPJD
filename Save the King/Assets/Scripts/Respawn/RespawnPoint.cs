using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Transform respawnPoint;

    void Start()
    {
        if (GameManager.instance != null)
        {
            // Set the respawn point to the position of this scene's entrance
            GameManager.instance.SetRespawnPoint(respawnPoint.transform);
        }
    }
}
