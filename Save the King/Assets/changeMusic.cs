using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMusic : MonoBehaviour
{
    public MusicManager.GameState gameState;
    private MusicManager musicManager;
    void Start(){
        musicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
    }
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player") && musicManager.currentState != gameState && musicManager.currentState != MusicManager.GameState.Combat){
            musicManager.SetGameState(gameState);
        }
    }
}
