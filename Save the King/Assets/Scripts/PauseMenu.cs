using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActions;
    public static bool GameIsPaused = false;
    bool inSettings = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    public Slider mouseSlider;
    public CinemachineFreeLook cam;

    public GameObject mouseGameObject;

    private PlayerControls controls;
    public Inventory inv;

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    private void Start()
    {
        SetMusicVolume();
        SetSFXVolume();
    }

    public static bool isPaused()
    {
        return GameIsPaused;
    }

    // Update is called once per frame
    void HandleBack()
    {
        if (!inv.isInInventory())
        {
            if (GameIsPaused)
            {
                if (inSettings)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
            else
            {
                Pause();
            }
        }
    }

    void Awake()
    {
        controls = InputManager.inputActions;
        controls.Gameplay.Back.performed += ctx => HandleBack();
    }

    public void Resume()
    {
        mouseGameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        Debug.Log("[TimeScale] Resuming time in PauseMenu");
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }


    public void Pause()
    {
        if (mouseGameObject != null)
        {
            Mouse mouse = mouseGameObject.GetComponent<Mouse>();
            mouse.optionsDisplayed = false;
            mouse.item = null;
        }

        mouseGameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        
        Debug.Log("[TimeScale] Pausing time in PauseMenu");
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        inSettings = false;
    }

    public void Settings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        inSettings = true;
    }

    public void ChangeSensitivity()
    {
        cam.m_XAxis.m_MaxSpeed = mouseSlider.value;
        cam.m_YAxis.m_MaxSpeed = mouseSlider.value / 200;
    }

    public void ExitGame()
    {
        Debug.Log("Exitting Game...");
    }

    public void ResetAllBindings()
    {
        cam.m_XAxis.m_MaxSpeed = 300f;
        cam.m_YAxis.m_MaxSpeed = 2f;
        mouseSlider.value = 300f;
    }
}
