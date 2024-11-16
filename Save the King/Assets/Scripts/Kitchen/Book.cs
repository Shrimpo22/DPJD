using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;
using TMPro;
public class Book : MonoBehaviour
{
    public GameObject bookClose;
    public GameObject bookOpen;
    private bool isLooking = false;
    public Camera myCamera;
    public Camera mainCamera;
    public GameObject freelockCamara;
    private CinemachineFreeLook freeLookComponent;
    GameObject player;
    void Start()
    {
        freeLookComponent = freelockCamara.GetComponent<CinemachineFreeLook>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isLooking){
            bookClose.SetActive(true);
            bookOpen.SetActive(false);
            player.SetActive(true);
            mainCamera.tag = "MainCamera";
            myCamera.tag="Untagged";
            mainCamera.gameObject.SetActive(true);
            myCamera.gameObject.SetActive(false);
            if (freeLookComponent != null)
            {
                freeLookComponent.enabled = true;
            }
            isLooking = false;
        }
    }
    
    public void openbook(){
        player.SetActive(false);
        bookClose.SetActive(false);
        bookOpen.SetActive(true);
        myCamera.tag = "MainCamera";
        mainCamera.tag="Untagged";
        mainCamera.gameObject.SetActive(false);
        myCamera.gameObject.SetActive(true);       
        isLooking = true;

    }
}
