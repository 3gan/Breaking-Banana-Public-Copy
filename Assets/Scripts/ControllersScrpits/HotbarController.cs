using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class HotbarController : MonoBehaviour
{

    public GameObject hotbarPanel;
    public GameObject slotPrefab;
    public int slotCount = 5;

    private ItemDictionary itemDictionary;

    private Key[] hotBarKeys;

    public int activeSlot = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        itemDictionary = FindAnyObjectByType<ItemDictionary>();
        hotBarKeys = new Key[slotCount];

        for(int i = 0; i < slotCount; i++)
        {
            hotBarKeys[i] = i < 4 ? (Key)((int)Key.Digit1 + i) : Key.Digit5;
        }


    }
    // Update is called once per frame
    void Update()
    {
        Slots[] slots = hotbarPanel.GetComponentsInChildren<Slots>();
        slots[activeSlot].GetComponent<Image>().color = Color.red;

        //scroll wheel through hotbar
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {

            activeSlot += 1;
            Debug.Log(activeSlot);
            if (activeSlot == 5)
            {
                activeSlot = 0;
            }
            SetActiveSlot(activeSlot);
            SetSlotColor(slots, activeSlot);

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {

            activeSlot -= 1;
            
            if (activeSlot == -1)
            {
                activeSlot = 4;
            }
            SetActiveSlot(activeSlot);
            SetSlotColor(slots, activeSlot);
        }

        for (int i = 0; i < slotCount; i++)
        {
            // set the text for the amount of item in the slot
            
            //get the key that was pressed and set it as the active slot
            if (Keyboard.current[hotBarKeys[i]].wasPressedThisFrame)
            {
                SetActiveSlot(i);
                //change color of selected slot
                SetSlotColor(slots, activeSlot);
            }
        }
    }

    private void SetSlotColor(Slots[] slots, int AS)
    {
        for (int j = 0; j < slotCount; j++)
        {
            slots[j].GetComponent<Image>().color = Color.white;
        }
        slots[AS].GetComponent<Image>().color = Color.red;
    }
    public void SetActiveSlot(int i)
    {
        activeSlot = i;
    }

    public int GetActiveSlot()
    {
        return activeSlot;
    }
    public int GetItemInSlot()
    {
        Slots slot = hotbarPanel.transform.GetChild(GetActiveSlot()).GetComponent<Slots>();
        if (slot.currentItem != null)
        {
            Item item = slot.currentItem.GetComponent<Item>();
            return item.GetItemID();
        }

        return 0;
    }

    public Item GetItemPrefabInSlot()
    {
        Slots slot = hotbarPanel.transform.GetChild(GetActiveSlot()).GetComponent<Slots>();
        if (slot.currentItem != null)
        {
            Item item = slot.currentItem.GetComponent<Item>();
            return item;
        }

        return null;
    }
    public List<InventorySaveData> GetHotBarItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        foreach (Transform slotTransform in hotbarPanel.transform)
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

    public void SetHotBarItems(List<InventorySaveData> invSaveData)
    {
        foreach (Transform child in hotbarPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, hotbarPanel.transform);
        }

        foreach (InventorySaveData data in invSaveData)
        {
            if (data.slotIndex < slotCount)
            {
                Slots slot = hotbarPanel.transform.GetChild(data.slotIndex).GetComponent<Slots>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }

            }
        }
    }

}



