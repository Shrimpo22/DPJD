using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject canvas;
    public GameObject Menu;
    public void PlayGame(){
        SceneManager.LoadSceneAsync("Banquet");
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void Settings(){
        canvas.SetActive(false);
        Menu.SetActive(true);
    }
    public void closeSettings(){
        canvas.SetActive(true);
        Menu.SetActive(false);
    }
}
