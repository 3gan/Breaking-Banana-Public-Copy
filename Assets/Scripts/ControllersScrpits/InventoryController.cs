using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;

    public GameObject hotBar;
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    // Start is called before the first frame update
    //NOTE*** update the special banana ids whenever you add one
    void Start()
    {
        itemDictionary = FindAnyObjectByType<ItemDictionary>();

        //for (int i = 0; i < slotCount; i++)
        //{
        //    Slots slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slots>();
        //    if( i < itemPrefabs.Length)
        //    {
        //        GameObject item = Instantiate(itemPrefabs[i], slot.transform);
        //        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //        slot.currentItem = item;
        //    }
        //}
    }

    public bool AddItem(GameObject itemPrefab)
    {
        //checks the hotbar first and then the inventory

        //look for slot with item already in it to stack
        foreach (Transform slotTransform in hotBar.transform)
        {
            Slots slot = slotTransform.GetComponent<Slots>();
            Item item = itemPrefab.GetComponent<Item>();
            //check is the item is stackable and stack if it is in the inventory
            if (item.GetItemID() == slot.GetCurrentItemID() && item.isStackable())
            {
                //check if the amount picked will stay under the max of item in inventory
                if (item.GetAmountInStack() + slot.GetCurrentItem().GetAmountInStack() < slot.GetCurrentItem().GetMaxStackAmount())
                {
                    
                    slot.GetCurrentItem().amountInStack += item.amountInStack;
                    slot.itemAmmountDisplay.text = slot.GetCurrentItem().GetAmountInStack().ToString(); // change text
                    return true;
                }
                else  //check if the amount will go over the max amount of the stack in the invenotry
                {
                  
                    int itemsTillMaxed = slot.GetCurrentItem().GetMaxStackAmount() - slot.GetCurrentItem().GetAmountInStack();
                    slot.GetCurrentItem().amountInStack += itemsTillMaxed;
                    slot.itemAmmountDisplay.text = slot.GetCurrentItem().GetAmountInStack().ToString(); // change text
                    item.amountInStack -= itemsTillMaxed;
                }
            }
        }

        //look for slot with item already in it to stack
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slots slot = slotTransform.GetComponent<Slots>();
            Item item = itemPrefab.GetComponent<Item>();
            //check is the item is stackable and stack if it is in the inventory
            if (item.GetItemID() == slot.GetCurrentItemID() && item.isStackable())
            {
                //check if the amount picked will stay under the max of item in inventory
                if(item.GetAmountInStack() + slot.GetCurrentItem().GetAmountInStack() < slot.GetCurrentItem().GetMaxStackAmount())
                {
                   
                    slot.GetCurrentItem().amountInStack += item.amountInStack;
                    slot.itemAmmountDisplay.text = slot.GetCurrentItem().GetAmountInStack().ToString(); // change text
                    return true;
                }
                else  //check if the amount will go over the max amount of the stack in the invenotry
                {
                   
                    int itemsTillMaxed = slot.GetCurrentItem().GetMaxStackAmount() - slot.GetCurrentItem().GetAmountInStack();
                    slot.GetCurrentItem().amountInStack += itemsTillMaxed;
                    slot.itemAmmountDisplay.text = slot.GetCurrentItem().GetAmountInStack().ToString(); // change text
                    item.amountInStack -= itemsTillMaxed;
                }
            }
        }

        //add to first empty slot in hot bar
        foreach (Transform slotTransform in hotBar.transform)
        {
            Slots slot = slotTransform.GetComponent<Slots>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                newItem.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                slot.currentItem = newItem;

                //only add to stack if its a item picked up for first time
                if (slot.currentItem.GetComponent<Item>().amountInStack == 0)
                {
                    slot.currentItem.GetComponent<Item>().amountInStack++;
                }
                slot.itemAmmountDisplay.text = slot.GetCurrentItem().GetAmountInStack().ToString();//change text
                return true;
            }
        }

        //add to first empty slot if hot bar is full
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slots slot = slotTransform.GetComponent<Slots>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                newItem.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                slot.currentItem = newItem;
                
                //only add to stack if its a item picked up for first time
                if(slot.currentItem.GetComponent<Item>().amountInStack == 0)
                {
                    slot.currentItem.GetComponent<Item>().amountInStack++;
                }
                slot.itemAmmountDisplay.text = slot.GetCurrentItem().GetAmountInStack().ToString();//change text
                return true;
            }
        }

        return false;
    }

    public int GetAmmountOfItemsInInventory(int ID)
    {
        int totalItemsInInvenotry = 0;
        
        //check hotbar
        foreach (Transform slotTransform in hotBar.transform)
        {
            Slots slot = slotTransform.GetComponent<Slots>();
            if (slot.GetCurrentItemID() == ID)
            {
                totalItemsInInvenotry += slot.GetCurrentItem().GetAmountInStack();
            }
        }

        //check inventory
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slots slot = slotTransform.GetComponent<Slots>();
            if (slot.GetCurrentItemID() == ID)
            {
                totalItemsInInvenotry += slot.GetCurrentItem().GetAmountInStack();
            }
        }

            return totalItemsInInvenotry;
    }

    public void RemoveItemsFromInventory(int ID, int ammount)
    {
        int ammountRemaining = ammount;
        //check hotbar
        foreach (Transform slotTransform in hotBar.transform)
        {
            
            Slots slot = slotTransform.GetComponent<Slots>();
            if(ammountRemaining == 0)
            {
                break;
            }

            if (slot.GetCurrentItemID() == ID)
            {
                if (ammountRemaining >= slot.GetCurrentItem().GetAmountInStack())
                {
                    Destroy(slot.transform.GetChild(1).gameObject);
                    
                    ammountRemaining -= slot.GetCurrentItem().GetAmountInStack();
                    slot.itemAmmountDisplay.text = "" + 0; // change text

                }
                else
                {
                    slot.GetCurrentItem().amountInStack -= ammountRemaining;
                    ammountRemaining = 0;
                    slot.itemAmmountDisplay.text = slot.GetCurrentItem().GetAmountInStack().ToString(); // change text
                }
                
            }
          
        }

        //check inventory
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slots slot = slotTransform.GetComponent<Slots>();
            if (slot.GetCurrentItemID() == ID)
            {
                slot.itemAmmountDisplay.text = slot.GetCurrentItem().GetAmountInStack().ToString(); // change text
                if (ammountRemaining >= slot.GetCurrentItem().GetAmountInStack())
                {
                    Destroy(slot.transform.GetChild(1).gameObject);
                    ammountRemaining -= slot.GetCurrentItem().GetAmountInStack();
                }
                else
                {
                    slot.GetCurrentItem().amountInStack -= ammount;
                }
               
            }
            
        }

    }
    public void removeFromSlot()
    {
        Slots slot = hotBar.GetComponent<HotbarController>().transform.GetChild(hotBar.GetComponent<HotbarController>().GetActiveSlot()).GetComponent<Slots>();
        Item item = hotBar.GetComponent<HotbarController>().GetItemPrefabInSlot();
        item.amountInStack--;
        slot.itemAmmountDisplay.text = slot.GetCurrentItem().GetAmountInStack().ToString(); // change text

        if (item.amountInStack <= 0)
        {
            Destroy(item.gameObject);
        }
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slots slot = slotTransform.GetComponent<Slots>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
            }
        }

        return invData;
    }

    public void SetInventoryItems(List<InventorySaveData> invSaveData)
    {
        foreach(Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        foreach(InventorySaveData data in invSaveData)
        {
            if(data.slotIndex < slotCount)
            {
                Slots slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slots>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
                if(itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }

            }
        }
    }
    public bool HasSpecialBananas()
    {
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slots slot = slotTransform.GetComponent<Slots>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                if (IsSpecialBanana(item.GetItemID()))  // Check if item is a special banana
                {
                    return true;
                }
            }
        }
        return false;
    }

    
    private bool IsSpecialBanana(int itemID)
    {
        int[] specialBananaIDs = { 8, 9, 10, 11, 13, 14, 18, 19 };  // NOTE*** special banana IDs
        foreach (int id in specialBananaIDs)
        {
            if (itemID == id)
                return true;
        }
        return false;
    }


}
