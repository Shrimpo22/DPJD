using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class book : MonoBehaviour
{
    [SerializeField] public List<GameObject> frontTexts; // Referências aos textos FrontText
    [SerializeField] public List<GameObject> backTexts;  // Referências aos textos BackText
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    int index = -1;
    int indice = 0;
    bool rotate = false;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject forwardButton;
    public GameObject start;


    private void Start()
    {
        InitialState();

    }

    public void InitialState()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].transform.rotation = Quaternion.identity;
        }
        pages[0].SetAsLastSibling();
        backButton.SetActive(false);

    }

    public void RotateForward()
    {
        if (rotate == true) { return; }
        index++;
        float angle = 180;
        ForwardButtonActions();
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));

    }

    public void ForwardButtonActions()
    {
        if (backButton.activeInHierarchy == false)
        {
            backButton.SetActive(true); //every time we turn the page forward, the back button should be activated
        }
        if (index == pages.Count - 1)
        {
            forwardButton.SetActive(false); //if the page is last then we turn off the forward button
            if (start != null)
                start.SetActive(true);
        }
    }

    public void RotateBack()
    {
        if (rotate == true) { return; }
        float angle = 0; //in order to rotate the page back, you need to set the rotation to 0 degrees around the y axis
        pages[index].SetAsLastSibling();
        BackButtonActions();
        StartCoroutine(Rotate(angle, false));
    }

    public void BackButtonActions()
    {
        if (forwardButton.activeInHierarchy == false)
        {
            forwardButton.SetActive(true); //every time we turn the page back, the forward button should be activated
        }
        if (index - 1 == -1)
        {
            backButton.SetActive(false); //if the page is first then we turn off the back button
        }
    }

    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;
        if (TryGetComponent(out AudioSource audioSource) && audioSource.clip != null)
        {
            audioSource.volume = 0.3f; // Ajuste o volume se necessário
            audioSource.Play();
        }
        UpdateTextVisibility(forward);
        while (true)
        {

            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, value); //smoothly turn the page
            float angle1 = Quaternion.Angle(pages[index].rotation, targetRotation); //calculate the angle between the given angle of rotation and the current angle of rotation

            if (angle1 < 0.1f)
            {
                if (forward == false)
                {
                    index--;
                }


                rotate = false;
                break;

            }

            yield return null;

        }
    }
    private void UpdateTextVisibility(bool x)
    {
        if (x)
        {

            if (indice + 1 == backTexts.Count)
            {
                backTexts[indice - 1].SetActive(false);
                frontTexts[indice].SetActive(false);

                backTexts[indice].SetActive(true);
            }
            else if (indice == 0)
            {
                backTexts[indice].SetActive(true);
                frontTexts[indice + 1].SetActive(true);
                frontTexts[indice].SetActive(false);

            }
            else
            {

                backTexts[indice - 1].SetActive(false);

                frontTexts[indice + 1].SetActive(true);
                frontTexts[indice].SetActive(false);

                backTexts[indice].SetActive(true);

            }

            indice++;

        }
        else
        {
            if (indice == backTexts.Count)
            {
                backTexts[indice - 1].SetActive(false);
                frontTexts[indice - 1].SetActive(true);
                backTexts[indice - 2].SetActive(true);
            }
            else if (indice - 1 == 0)
            {

                backTexts[indice - 1].SetActive(false);
                frontTexts[indice - 1].SetActive(true);
                frontTexts[indice].SetActive(false);

            }
            else
            {
                frontTexts[indice].SetActive(false);
                frontTexts[indice - 1].SetActive(true);
                if (indice - 2 >= 0)
                {
                    backTexts[indice - 1].SetActive(false);
                    backTexts[indice - 2].SetActive(true);
                }
            }
            indice--;
        }
    }



    public void startGame()
    {
        SceneManager.LoadSceneAsync("FullGame");
    }


}
