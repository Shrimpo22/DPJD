        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{

    [SerializeField] private SceneField[] _scenesToLoad;
    [SerializeField] private SceneField[] _scenesToUnload;

    private GameObject _player;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider collision)
    {   
        if (collision.gameObject == _player)
        {
            SaveCurrentSceneState();
            LoadScenes();
            UnloadScenes();
        }
    }

    private void LoadScenes()
    {
        for (int i = 0; i < _scenesToLoad.Length; i++)
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == _scenesToLoad[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }

            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(_scenesToLoad[i], LoadSceneMode.Additive);
                RestoreSceneState(_scenesToLoad[i]);
            }
        }
    }

    private void UnloadScenes()
    {
        for (int i = 0; i < _scenesToUnload.Length; i++)
        {
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == _scenesToUnload[i].SceneName)
                {
                    Debug.Log("unnloading " + loadedScene.name);
                    SceneManager.UnloadSceneAsync(_scenesToUnload[i]);
                }
            }
        }
    }

        private void SaveCurrentSceneState()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SaveSceneState(currentScene.name);  // Save the current scene state
    }

        private void SaveSceneState(string sceneName)
    {
        SceneState sceneState = new();

        // Get the EnemyManager and save the state of all enemies
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            foreach (var enemyData in enemyManager.enemies)
            {
                    EnemyDataState enemyState = new()
                    {
                        position = enemyData.enemyPrefab.transform.position,
                        rotation = enemyData.enemyPrefab.transform.rotation,
                        enemyObject = enemyData.enemyPrefab // Save reference to the actual GameObject
                    };
                    sceneState.enemyDataStates.Add(enemyState);
            }
        }
        else
        {
            Debug.Log("There is no Enemy Manager...");
        }

    // Save the state of all interactable objects by position
        Interactable[] interactables = FindObjectsOfType<Interactable>();
        foreach (Interactable interactable in interactables)
        {
            // Save the GameObject's position and active state
            GameObject interactableObject = interactable.gameObject;
            bool isActive = interactableObject.activeSelf;
            sceneState.interactableObjects.Add(new GameObjectState
            {
                position = interactableObject.transform.position,
                isActive = isActive
            });
        }

        GameManager.Instance.SaveSceneState(sceneName, sceneState);
    }

    private void RestoreSceneState(string sceneName)
    {
        SceneState sceneState = GameManager.Instance.LoadSceneState(sceneName);

        if (sceneState == null)
            return;

        // Restore enemy states using EnemyManager
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            for (int i = 0; i < enemyManager.enemies.Count; i++)
            {
                var enemyData = enemyManager.enemies[i];
                var enemyState = sceneState.enemyDataStates[i];

                // Invoke a method in the AiAgent to restore its internal state
                enemyData.enemyPrefab.GetComponent<AiAgent>().RestoreState(enemyState.position, enemyState.rotation, enemyData.enemyPrefab.GetComponent<AiAgent>().isDead);

            }
        }

        // Restore interactable objects by position
        foreach (GameObjectState objectState in sceneState.interactableObjects)
        {
            // Find the interactable object in the scene by its position
            foreach (Interactable interactable in FindObjectsOfType<Interactable>())
            {
                GameObject interactableObject = interactable.gameObject;

                // Check if the position is approximately equal to the saved position
                if (Vector3.Distance(interactableObject.transform.position, objectState.position) < 0.1f)
                {
                    // Restore the active state
                    interactableObject.SetActive(objectState.isActive);
                    break; // Found the matching object, no need to continue the loop
                }
            }
        }
    }
}
