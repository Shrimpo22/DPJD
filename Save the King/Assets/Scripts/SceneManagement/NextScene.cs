using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{

    public string targetScene; // The scene this door leads to

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
    
            //GameManager.Instance.SaveCurrentSceneState();
            Debug.Log("triggered tp");
            GameManager.Instance.TeleportToNextScene(targetScene);
        }
    }
}
