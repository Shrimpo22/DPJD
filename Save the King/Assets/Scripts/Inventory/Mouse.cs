using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    public Image mouseCursor;
    public Item item;
    public GameObject InspectManager;
    public Image imagemItem; 
    public TextMeshProUGUI descricaoItem;
    public Button opcoes;

    public GameObject inventory;
    public Button fechar;
    
    private bool optionsDisplayed = false;

    void Update()
    {
        mouseCursor.transform.position = Input.mousePosition;

        if (!inventory.activeSelf)
        {
            FecharInspectManager();
        }

        if (item != null && !InspectManager.activeSelf && !optionsDisplayed)
        {
            ShowOpcoes();
            optionsDisplayed = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(opcoes.GetComponent<RectTransform>(), Input.mousePosition))
            {
                opcoes.gameObject.SetActive(false);
                optionsDisplayed = false;
            }
        }

        if (opcoes.gameObject.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(opcoes.GetComponent<RectTransform>(), Input.mousePosition))
            {
                OnClick();
                opcoes.gameObject.SetActive(false);
                optionsDisplayed = false;
            }
        }
    }

    public void ShowOpcoes()
    {
        opcoes.gameObject.SetActive(true);
        opcoes.transform.position = Input.mousePosition;
    }

    public void OnClick()
    {
        if (item != null)
        {
            InspectManager.SetActive(true);
        }
    }

    public void SetUI()
    {
        if (item != null)
        {
            Sprite itemSprite = item.GiveItemImage();
            Debug.Log("Imagem do item: " + itemSprite);

            descricaoItem.text = item.GiveDiscresp(); 

            if (itemSprite != null)
            {
                imagemItem.sprite = itemSprite;
                Canvas.ForceUpdateCanvases();
            }
            else
            {
                Debug.LogWarning("O sprite do item retornado é nulo!");
                imagemItem.sprite = null;
            }
        }
        else
        {
            Debug.LogWarning("itemSlot ou item está nulo!");
            imagemItem.sprite = null;
        }
    }

    public void FecharInspectManager() 
    {
        InspectManager.SetActive(false);
        item = null;
    }
}