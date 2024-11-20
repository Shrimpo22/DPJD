using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource puzzleMusic;
    public AudioSource combatMusic;
    public AudioSource explorationMusic;
    public AudioSource chapelMusic;
    
    private AudioSource currentMusic;
    private Coroutine transitionCoroutine;
    
    public enum GameState { Puzzle, Combat, Exploration, Chapel, Boss, Dungeon, Menu }
    public GameState currentState;
    public GameState auxState;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetGameState(GameState.Exploration); // Start with exploration music by default
    }

    public void Update(){
        if(auxState != currentState){
            SetGameState(auxState);
        }
    }

    public void SetGameState(GameState newState)
    {
        if (newState == currentState) return;

        currentState = newState;
        auxState = currentState;
        
        // Choose the appropriate music track
        switch (currentState)
        {
            case GameState.Puzzle:
                PlayMusic(puzzleMusic);
                break;
            case GameState.Combat:
                PlayMusic(combatMusic);
                break;
            case GameState.Exploration:
                PlayMusic(explorationMusic);
                break;
            case GameState.Chapel:
                PlayMusic(chapelMusic);
                break;
        }
    }

    private void PlayMusic(AudioSource newMusic)
    {
        if (currentMusic == newMusic) return;

        // Stop any ongoing transition
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);

        // Start crossfade transition
        transitionCoroutine = StartCoroutine(CrossfadeMusic(newMusic));
    }

    private IEnumerator CrossfadeMusic(AudioSource newMusic)
    {
        if (currentMusic != null)
        {
            // float fadeOutDuration = 1.0f;
            // for (float t = 0; t < fadeOutDuration; t += Time.deltaTime)
            // {
            //     currentMusic.volume = Mathf.Lerp(1, 0, t / fadeOutDuration);
                 yield return null;
            // }
            currentMusic.Stop();
            currentMusic.volume = 0.05f;
        }

        currentMusic = newMusic;
        currentMusic.Play();

        // float fadeInDuration = 1.0f;
        // for (float t = 0; t < fadeInDuration; t += Time.deltaTime)
        // {
        //     currentMusic.volume = Mathf.Lerp(0, 0.05f, t / fadeInDuration);
            yield return null;
        // }
    }
}
