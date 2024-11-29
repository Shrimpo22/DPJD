using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{

    public string targetScene; // The scene this door leads to
    public Vector3 targetPosition; // Where the player will appear in the new scene

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
    
            //GameManager.Instance.SaveCurrentSceneState();
            StartCoroutine(GameManager.Instance.LoadNewScene(targetScene, targetPosition));
        }
    }
}
