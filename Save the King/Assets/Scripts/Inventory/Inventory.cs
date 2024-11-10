using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using Cinemachine;
public class Inventory : MonoBehaviour
{
    [SerializeReference] public List<ItemSlotInfo> items = new List<ItemSlotInfo>();

    [Space]
    [Header("Inventory Menu Components")]

    public GameObject InventoryMenu;

    public GameObject ItemPanel;
    private PlayerControls controls;
    public AudioSource audioSource;
    public GameObject ItemPanelGrid;
    public Mouse mouse;
    public bool isLookingAtMap = false;
    Dictionary<string, Item> allItemsDictionary = new Dictionary<string, Item>();
    private List<ItemPanel> existingPanels = new List<ItemPanel>();
    
    private CinemachineFreeLook freeLookCamera;
    [Space]

    private bool wasOpenByOtherEvent = false;
    public int InventorySize = 12;
    void Start()
    {
        controls = new PlayerControls();
        if(InventoryMenu.activeSelf)
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
        foreach(Item i in allItems)
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
        //AddItem("Potions",6);
        RefreshInventory();
        
        
    }
    

    public void OpenIt(){
        wasOpenByOtherEvent = true;
    }
    void Update()
    {
        if(InventoryMenu.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
            {
                    InventoryMenu.SetActive(false);
                    Cursor.visible = false; 
                    if (freeLookCamera != null)
                    {
                        freeLookCamera.enabled = true; 
                    }
                    audioSource.Play();
                    Time.timeScale = 1; 
                    //controls.Gameplay.Camera.Enable();

            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.I) || wasOpenByOtherEvent)
            {    
                wasOpenByOtherEvent = false;
                InventoryMenu.SetActive(true); 
                Cursor.lockState = CursorLockMode.None; 
                Cursor.visible = true; 
                if (freeLookCamera != null)
                {
                    freeLookCamera.enabled = false; 
                }
              // controls.Gameplay.Camera.Disable();
                Time.timeScale = 0;
                audioSource.Play();
            }

        }
        
        
    }

    public void RefreshInventory()
    {
        existingPanels = ItemPanelGrid.GetComponentsInChildren<ItemPanel>().ToList();

        if(existingPanels.Count < InventorySize)
        {
            int amoutToCount = InventorySize - existingPanels.Count;
            for(int  i = 0; i < amoutToCount; i++)
            {
                GameObject newPanel = Instantiate(ItemPanel, ItemPanelGrid.transform);
                existingPanels.Add(newPanel.GetComponent<ItemPanel>());
            }
        }

        int index= 0;
        foreach(ItemSlotInfo i in items)
        {
            i.name = "" + (index+1);
            if(i.item != null) i.name += ": " + i.item.GiveName();
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
        foreach(ItemSlotInfo i in items)
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
        foreach(ItemSlotInfo i in items)
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
        if(slot.stacks>1){
        slot.stacks = slot.stacks - 1;
        }else{
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

