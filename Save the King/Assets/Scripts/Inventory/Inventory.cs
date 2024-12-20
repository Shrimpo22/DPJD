using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using Cinemachine;
using Unity.VisualScripting;
using System;
public class Inventory : MonoBehaviour
{
    [SerializeReference] public List<ItemSlotInfo> items = new List<ItemSlotInfo>();

    [Space]
    [Header("Inventory Menu Components")]

    public GameObject InventoryMenu;
    private bool wasOpenByOtherEvent = false;
    public GameObject ItemPanel;
    private PlayerControls controls;
    public GameObject Mouse;
    public AudioSource audioSource;
    public GameObject ItemPanelGrid;
    public Mouse mouse;
    public bool isZoomedIn = false;
    public bool isLookingAtCook = false;
    public bool isLookingAtOrgan = false;
    public bool isLookingAtCandle = false;
    public bool isLookingAtStatue = false;
    public bool isLookingAtWood = false;
    
    public bool isLookingAtCandleHolder = false;
    public bool isLookingAtMap = false;
    public bool isLookingAtMirror = false; 
    private Mouse mousse;
    Dictionary<string, Item> allItemsDictionary = new Dictionary<string, Item>();
    private List<ItemPanel> existingPanels = new List<ItemPanel>();

    private CinemachineFreeLook freeLookCamera;
    [Space]

    public int InventorySize = 12;
    public bool inInventory = false;

    public bool isInInventory()
    {
        return inInventory;
    }

    void Start()
    {
        if (InventoryMenu.activeSelf)
        {
            InventoryMenu.SetActive(false);
        }
        freeLookCamera = FindObjectOfType<CinemachineFreeLook>();

        for (int i = 0; i < InventorySize; i++)
        {
            items.Add(new ItemSlotInfo(null, 0));
        }

        List<Item> allItems = GetAllItems().ToList();
        string itemsInDictionary = "Items in Dictionary: ";
        foreach (Item i in allItems)
        {
            if (!allItemsDictionary.ContainsKey(i.GiveName()))
            {
                allItemsDictionary.Add(i.GiveName(), i);
                itemsInDictionary += ", " + i.GiveName();
            }
            else
            {
                Debug.Log("" + i + " already exists in Dictionary - shares name with " + allItemsDictionary[i.GiveName()]);
            }
        }
        itemsInDictionary += ".";
        Debug.Log(itemsInDictionary);
        AddItem("Potions", 2);
        RefreshInventory();


    }

    void Awake()
    {
        controls = InputManager.inputActions;
        controls.Gameplay.Inventory.performed += ctx => HandleInventory();
        controls.Gameplay.Back.canceled += ctx => HandleClosing();
    }

    public void OpenIt()
    {
        isZoomedIn = true;
    }
    
    public void closeInventory()
    {
        GameObject m = GameObject.FindGameObjectWithTag("mousse");
        
        if(m != null ){
            Debug.Log("[Mouse] Mouse being hidden by Inv");
            mousse = m.GetComponent<Mouse>();
            mousse.optionsDisplayed = false;
            mousse.item = null;
        }

        GameObject opcoes = GameObject.FindGameObjectWithTag("opcoes");
        if (opcoes != null)
        {
            opcoes.SetActive(false);
        }
        
        
        GameObject taskmanager = GameObject.FindGameObjectWithTag("TaskManager");
        if (taskmanager != null)
        {
            taskmanager.SetActive(false);
        }
       
        wasOpenByOtherEvent = false;
        InventoryMenu.SetActive(false);
        Cursor.visible = false;
        if (freeLookCamera != null)
        {
            freeLookCamera.enabled = true;
        }
        audioSource.Play();
        Debug.Log("[TimeScale] Resuming time in Inv");
        Time.timeScale = 1;
        inInventory = false;
        Mouse.SetActive(false);
        
    }

    void HandleClosing()
    {
        try{
            if (!PauseMenu.isPaused())
            {
                if (InventoryMenu.activeSelf)
                {
                    closeInventory();
                }
            }
        }catch(Exception){
            Debug.Log("Lá excepcion" + InventoryMenu);
            InventoryMenu = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(obj => obj.name == "Inventroy Menu");
            Debug.Log("tratando excepcion" + InventoryMenu.name);
            if (!PauseMenu.isPaused())
            {
                if (InventoryMenu.activeSelf)
                {
                    closeInventory();
                }
            }
        }
    }

    public void openInventory()
    {
        Debug.Log("[Mouse] Mouse being showned by Inv");
        if(Mouse == null){
            Start();
        }
        Mouse.SetActive(true);
        inInventory = true;
        wasOpenByOtherEvent = false;
        InventoryMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (freeLookCamera != null)
        {
            freeLookCamera.enabled = false;
        }
        // controls.Gameplay.Camera.Disable();
        Debug.Log("[TimeScale] Pausing time in Inv");
        Time.timeScale = 0;
        audioSource.Play();
    }

    void HandleInventory()
    {

        if (!PauseMenu.isPaused() && !isZoomedIn)
        {
            if (InventoryMenu.activeSelf)
            {
                closeInventory();

            }
            else
            {
                
                openInventory();
            }
        }
    }

    public void RefreshInventory()
    {
        if(existingPanels == null){
            Start();
        }
        existingPanels = ItemPanelGrid.GetComponentsInChildren<ItemPanel>().ToList();

        if (existingPanels.Count < InventorySize)
        {
            int amoutToCount = InventorySize - existingPanels.Count;
            for (int i = 0; i < amoutToCount; i++)
            {
                GameObject newPanel = Instantiate(ItemPanel, ItemPanelGrid.transform);
                existingPanels.Add(newPanel.GetComponent<ItemPanel>());
            }
        }

        int index = 0;
        foreach (ItemSlotInfo i in items)
        {
            i.name = "" + (index + 1);
            if (i.item != null) i.name += ": " + i.item.GiveName();
            else i.name += ": -";

            //Update our Panels
            ItemPanel panel = existingPanels[index];
            panel.name = i.name + " Panel";
            if (panel != null)
            {
                panel.inventory = this;
                panel.itemSlot = i;
                if (i.item != null)
                {
                    panel.itemImage.gameObject.SetActive(true);
                    panel.itemImage.sprite = i.item.GiveItemImage();
                    panel.itemImage.CrossFadeAlpha(1, 0.05f, true);
                    panel.stacksText.gameObject.SetActive(true);
                    panel.stacksText.text = "" + i.stacks;
                }
                else
                {
                    panel.itemImage.gameObject.SetActive(false);
                    panel.stacksText.gameObject.SetActive(false);
                }
            }
            index++;
        }


    }

    public void DropItemByName(string itemName)
    {
        // Procura o último slot que contém o item com o nome fornecido
        ItemSlotInfo slotToDrop = items.LastOrDefault(slot => slot.item != null && slot.item.GiveName() == itemName);

        if (slotToDrop == null)
        {
            Debug.Log("Não foi possível encontrar o item " + itemName + " no inventário.");
            return;
        }

        // Limpa o slot
        ClearSlot(slotToDrop);

        Debug.Log("Item " + itemName + " removido do inventário.");

        // Atualiza a UI do inventário
        RefreshInventory();
    }

    public bool HasItemNamed(string itemName)
    {
        ItemSlotInfo slotToDrop = items.LastOrDefault(slot => slot.item != null && slot.item.GiveName() == itemName);
        if (slotToDrop == null)
        {
            return false;
        }
        return true;
    }

    public int AddItem(string itemName, int amount)
    {
        Debug.Log("Adding... " + itemName + " " + amount);
        //Find Item to add
        Item item = null;
        allItemsDictionary.TryGetValue(itemName, out item);
        //Exit method if no Item was found
        if (item == null)
        {
            Debug.Log("Could not find Item in Dictionary to add to Inventory");
            return amount;
        }

        //Check for open spaces in existing slots
        foreach (ItemSlotInfo i in items)
        {
            if (i.item != null)
            {
                if (i.item.GiveName() == item.GiveName())
                {
                    if (amount > i.item.MaxStacks() - i.stacks)
                    {
                        amount -= i.item.MaxStacks() - i.stacks;
                        i.stacks = i.item.MaxStacks();
                    }
                    else
                    {

                        i.stacks += amount;
                        //if (InventoryMenu.activeSelf)
                        RefreshInventory();
                        return 0;
                    }
                }
            }
        }
        //Fill empty slots with leftover items
        foreach (ItemSlotInfo i in items)
        {
            if (i.item == null)
            {
                if (amount > item.MaxStacks())
                {
                    i.item = item;
                    i.stacks = item.MaxStacks();
                    amount -= item.MaxStacks();
                }
                else
                {

                    i.item = item;
                    i.stacks = amount;
                    //if (InventoryMenu.activeSelf) 
                    RefreshInventory();
                    return 0;
                }
            }
        }
        //No space in Inventory, return remainder items
        Debug.Log("No space in Inventory for: " + item.getNameWithSpaces());
        if (InventoryMenu.activeSelf) RefreshInventory();
        return amount;
    }

    public void ClearSlot(ItemSlotInfo slot)
    {
        if (slot.stacks > 1)
        {
            slot.stacks = slot.stacks - 1;
        }
        else
        {
            slot.item = null;
            slot.stacks = 0;
        }
    }

    IEnumerable<Item> GetAllItems()
    {
        return System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsSubclassOf(typeof(Item)))
            .Select(type => System.Activator.CreateInstance(type) as Item);
    }
}

