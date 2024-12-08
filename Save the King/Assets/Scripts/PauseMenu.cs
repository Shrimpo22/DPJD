using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActions;
    public static bool GameIsPaused = false;
    bool inSettings = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    [SerializeField] private Slider mouseSlider;
    public CinemachineFreeLook cam;

    private PlayerControls controls;
    public Inventory inv;
    
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public void SetMusicVolume(){
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(){
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadSettings(){
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        mouseSlider.value = PlayerPrefs.GetFloat("cameraSensitivity");

        SetMusicVolume();
        SetSFXVolume();
        ChangeSensitivity();
    }

    public void ChangeSensitivity(){
        float sensitivity = mouseSlider.value;
        PlayerPrefs.SetFloat("cameraSensitivity", sensitivity);
        if(cam != null){
            cam.m_XAxis.m_MaxSpeed = sensitivity;
            cam.m_YAxis.m_MaxSpeed = sensitivity/200;
        }
    }


    private void Start(){
        Debug.Log("ENTROU");
        if(PlayerPrefs.HasKey("musicVolume")){
            LoadSettings();
        }else{
            SetMusicVolume();
            SetSFXVolume();
            ChangeSensitivity();
        }
    }

    public static bool isPaused(){
        return GameIsPaused;
    }

    // Update is called once per frame
    void HandleBack()
    {
        if (this == null || pauseMenuUI == null) return;
        
        if(!inv.isInInventory()){
            if (GameIsPaused){
                if (inSettings){
                    Pause();
                } else {
                    Resume();
                }
            } else {
                Pause();
            }
        }
    }

    void Awake()
    {
        controls = InputManager.inputActions;
        controls.Gameplay.Back.performed += ctx => HandleBack();
    }

    public void Resume (){
        Debug.Log("[TimeScale] Resuming time in PauseMenu");
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }


    public void Pause (){
        Debug.Log("[TimeScale] Pausing time in PauseMenu");
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        inSettings = false;
        
    }

    public void Settings (){
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        inSettings = true;
    }



    public void ExitGame (){
        Debug.Log("Exitting Game...");
    }

    public void ResetAllBindings()
    {
        mouseSlider.value = 300f;
        ChangeSensitivity();

        musicSlider.value = 0.75f;
        SetMusicVolume();

        sfxSlider.value = 0.75f;
        SetSFXVolume();
    }

}
