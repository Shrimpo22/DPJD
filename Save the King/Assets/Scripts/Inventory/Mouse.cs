using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class Mouse : MonoBehaviour
{
    public Image mouseCursor;
    public Item item;
    public GameObject InspectManager;
    public Image imagemItem; 
    public TextMeshProUGUI descricaoItem;
    
    public GameObject opcoes;
    public AudioSource audioSource;
    public Button inspect;

    public GameObject use; 
    public GameObject inventory;
    public Button fechar;
    public TMP_Text titulo;
    public bool optionsDisplayed = false;

    void Update()
    {
        mouseCursor.transform.position = Input.mousePosition;

       if (!inventory.activeSelf)
        {
            opcoes.gameObject.SetActive(false);
            InspectManager.gameObject.SetActive(false);
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
                item = null;
            }
        }

        if (opcoes.gameObject.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(inspect.GetComponent<RectTransform>(), Input.mousePosition))
            {
                OnClick();
                opcoes.gameObject.SetActive(false);
                optionsDisplayed = false;
            }
        }
    }

    public void ShowOpcoes()
    {
        
        if (item.GiveName().StartsWith("Key") || 
        (item.GiveName().StartsWith("Statu") && GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().isLookingAtStatue == false)||
        (item.GiveName().StartsWith("Candle") && GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().isLookingAtCandleHolder == false)||
        (item.GiveName().StartsWith("SheetMusic") && GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().isLookingAtOrgan == false)||
        (item.GiveName().StartsWith("MapPiece") && GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().isLookingAtMap == false) ||
        (item.GiveName().Contains("Item") && GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().isLookingAtCook == false) ||
        (item.GiveName().StartsWith("Plate") && GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().isLookingAtCook == false) ||
        (item.GiveName().StartsWith("Glass") && GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().isLookingAtMirror == false)
        )
        {
            
            audioSource.Play();
            opcoes.gameObject.SetActive(true);
            opcoes.transform.position = Input.mousePosition;
            use.gameObject.SetActive(false);
        }
        else
        {
            
            audioSource.Play();
            opcoes.gameObject.SetActive(true);
            opcoes.transform.position = Input.mousePosition;
            use.gameObject.SetActive(true);
        }
    }

    public void OnClick()
    {
        if (item != null)
        {
            audioSource.Play();
            InspectManager.SetActive(true);
        }
    }

    public void SetUI()
    {
        if (item != null)
        {
            Sprite itemSprite = item.GiveItemImage();
            
            titulo.text = item.getNameWithSpaces();
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
        audioSource.Play();

        InspectManager.SetActive(false);
        item = null;
    }

    public void UseFunction()
    {
        audioSource.Play();
        Action useAction = item.UseFunc(); 
        useAction?.Invoke(); 

        item = null;
        opcoes.SetActive(false);
        optionsDisplayed = false;
    }
}