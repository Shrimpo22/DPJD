using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource puzzleMusic;
    public AudioSource[] combatMusics;
    public AudioSource explorationMusic;
    public AudioSource chapelMusic;
    public AudioSource firstBossMusic;
    public AudioSource secondBossMusic;
    public AudioSource throneCorridor;
    public AudioSource ending;
    public AudioSource mainMenu;

    private AudioSource currentMusic;
    private GameState previousState;
    private Coroutine transitionCoroutine;

    public enum GameState { Puzzle, Combat, Exploration, Chapel, Dungeon, Menu, Boss1, Boss2, throneCorridor, Ending, MainMenu }
    public GameState currentState;
    public GameState auxState;
    private PlayerMovement player;

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
        GameObject aux = GameObject.FindGameObjectWithTag("Player");
        if (aux != null)
            player = aux.GetComponent<PlayerMovement>();
        SetGameState(currentState); // Start with exploration music by default
    }

    public void LateUpdate()
    {
        if (player != null){
            
        if (player.IsDetected())
            {
                if (currentState != GameState.Combat)
                {
                    SetGameState(GameState.Combat);
                }
            }
            else if (currentState == GameState.Combat)
            {
                SetGameState(previousState);
            }
        }

        if (auxState != currentState)
        {
            SetGameState(auxState);
        }
    }

    public void SetGameState(GameState newState)
    {
        if (newState == currentState) return;
        previousState = currentState;
        currentState = newState;
        auxState = currentState;

        // Choose the appropriate music track
        switch (currentState)
        {
            case GameState.Puzzle:
                PlayMusic(puzzleMusic);
                break;
            case GameState.Combat:
                Debug.Log(combatMusics.Length);
                int rand = Random.Range(0, combatMusics.Length);
                PlayMusic(combatMusics[rand]);
                break;
            case GameState.Exploration:
                PlayMusic(explorationMusic);
                break;
            case GameState.Chapel:
                PlayMusic(chapelMusic);
                break;
            case GameState.Boss1:
                player.EnterThrone();
                PlayMusic(firstBossMusic);
                break;
            case GameState.Boss2:
                PlayMusic(secondBossMusic);
                break;
            case GameState.throneCorridor:
                Debug.Log("Aqui entro");
                player.EnterThroneCorridor(2f);
                PlayMusic(throneCorridor);
                break;
            case GameState.Ending:
                PlayMusic(ending);
                break;

            case GameState.MainMenu:
                Debug.Log("AAAAAAAAAA");
                PlayMusic(mainMenu);
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
