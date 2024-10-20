using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mouse : MonoBehaviour
{
    public Image mouseCursor;
    public Item item;
    public GameObject InspectManager;
    public Image imagemItem; 
    public TextMeshProUGUI descricaoItem;
    public Button opcoes;

    public Button fechar;

    void Update()
    {
        mouseCursor.transform.position = Input.mousePosition;

        
        if (item != null)
        {
            if (!InspectManager.activeSelf) 
            {
                ShowOpcoes(); 
            }
            else
            {
                opcoes.gameObject.SetActive(false); 
            }
        }

       
        if (opcoes.gameObject.activeSelf && Input.GetMouseButtonDown(0)) 
        {
            OnClick(); 
            opcoes.gameObject.SetActive(false); 
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
