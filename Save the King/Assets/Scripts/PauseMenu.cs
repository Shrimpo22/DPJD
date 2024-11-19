using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActions;
    public static bool GameIsPaused = false;
    bool inSettings = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    public Slider xAxis, yAxis;
    public CinemachineFreeLook cam;

    private PlayerControls controls;
    public Inventory inv;
    

    public static bool isPaused(){
        return GameIsPaused;
    }

    // Update is called once per frame
    void HandleBack()
    {
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }


    public void Pause (){
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

    public void ChangeXAxis(){
        cam.m_XAxis.m_MaxSpeed = xAxis.value;
    }

    public void ChangeYAxis(){
        cam.m_YAxis.m_MaxSpeed = yAxis.value;
    }

    public void ExitGame (){
        Debug.Log("Exitting Game...");
    }

    public void ResetAllBindings()
    {
        cam.m_XAxis.m_MaxSpeed = 300f;
        xAxis.value = 300f;
        cam.m_YAxis.m_MaxSpeed = 2f;
        yAxis.value = 2f;
    }
}
