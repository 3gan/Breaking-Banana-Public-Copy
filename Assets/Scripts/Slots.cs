using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public GameObject currentItem;
    public TMP_Text itemAmmountDisplay;
    public GameObject itemName;
    public TMP_Text text;
    public void Update()
    {
        if(currentItem != null)
        {
            text.text = currentItem.GetComponent<Item>().Name;
        }
        else
        {
            text.text = "";
        }
      
    }
    public Item GetCurrentItem()
    {
        return currentItem.GetComponent<Item>();
    }
    public int GetCurrentItemID()
    {
        if(currentItem == null)
        {
            return 0;
        }
        return currentItem.GetComponent<Item>().GetItemID();
    }

    public void diplayName()
    {
        if(currentItem != null)
        {
            itemName.SetActive(true);
        }
        
    }

    public void HideName()
    {
        if(currentItem != null)
        {
            itemName.SetActive(false);
        }
        
    }
}
