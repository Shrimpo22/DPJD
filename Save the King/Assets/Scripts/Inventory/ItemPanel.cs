using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class ItemPanel : MonoBehaviour, IPointerClickHandler
{
    public Inventory inventory;
    public ItemSlotInfo itemSlot;
    public Image itemImage;
    public TextMeshProUGUI stacksText;

    private Mouse mouse; // Referência ao Mouse

    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventory != null)
        {
            mouse = inventory.mouse; 

            if (eventData.button == PointerEventData.InputButton.Left && itemSlot.item != null)
            {   
                
                mouse.item = itemSlot.item; 
                mouse.SetUI(); 
                
            }
            else
            {
                Debug.LogWarning("Nenhum item encontrado no itemSlot!");
            }
        }
    }
}