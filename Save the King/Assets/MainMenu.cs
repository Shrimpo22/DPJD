using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject canvas;
    public GameObject Menu;
    public GameObject room;
    public GameObject maincamara;
    public GameObject camara;
    public GameObject book;

    public void Start(){
        Destroy(GameObject.Find("GameManager"));
    }
    public void PlayGame(){
        room.SetActive(true);
        maincamara.SetActive(false);
        camara.SetActive(true);
        book.SetActive(true);
        canvas.SetActive(false);
        Menu.SetActive(false);  
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
