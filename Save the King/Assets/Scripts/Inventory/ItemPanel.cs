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

    // Este método será chamado automaticamente quando o painel for clicado
    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventory != null)
        {
            mouse = inventory.mouse; // Atribui o mouse a partir do inventário

            // Verifica se o clique foi com o botão esquerdo e se o painel tem um item
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