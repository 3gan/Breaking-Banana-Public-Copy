using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    public bool stackable;
    public int maxStackAmount;
    public int amountInStack;
    public TMP_Text stackAmmountText;
    public int price = 1;
    public float growthMultplier = 1;
    public bool isPlantingBananan;
    public void Awake()
    {
        stackAmmountText.text = amountInStack.ToString();
    }
    public void SetStackText(string text)
    {
        stackAmmountText.text = text; 
    }
    public int GetItemID()
    {
        return ID;
    }
    public bool isStackable()
    {
        return stackable;
    }

    public int GetMaxStackAmount()
    {
        return maxStackAmount;
    }
    public int GetAmountInStack()
    {
        return amountInStack;
    }
}
