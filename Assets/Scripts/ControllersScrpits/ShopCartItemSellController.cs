using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ShopCartItemSellController : MonoBehaviour
{
    public int amount = 1; //the amount of items being purchased
    public int cost; //the total cost of the item being purchased
    private CartController cart;

    public int price; //the price of the individual item being purchased
    public TMP_Text amountText;
    public TMP_Text costText;
    public GameObject item;// the item prefab being purchased
    public GameObject bananasMaxed;

    private InventoryController inventoryController;
    private void Start()
    {
        cart = FindAnyObjectByType<CartController>();
        inventoryController = FindAnyObjectByType<InventoryController>();
        bananasMaxed.SetActive(false);
        cost = price;
    }


    public void IncreaseAmmount()
    {
        int bananasInInventory = inventoryController.GetAmmountOfItemsInInventory(item.GetComponent<Item>().GetItemID());
        amount++;
        if (amount <= bananasInInventory)
        {
            cost = price * amount;
            amountText.text = "x" + amount;
            costText.text = "$" + cost;
            cart.SetTotalCost(price);
        }
        else
        {
            amount--;
            bananasMaxed.SetActive(true);
            StartCoroutine(DiplayFade());
        }
    }
    public void DecreaseAmmount()
    {
      
        amount--;
        if(amount <= 0)
        {
            Destroy(gameObject);
            cart.SetTotalCost(-price);
            if (cart.GetTotalCost() == 0)
            {
                cart.SetCartText("Your cart is empty");
            }
        }
        else
        {
            cost = price * amount;
            amountText.text = "x" + amount;
            costText.text = "$" + cost;
            cart.SetTotalCost(-price);
        }
    
    }

    public void setAmount(int i)
    {
        amount = i;
        cost = price * amount;
        amountText.text = "x" + amount;
        costText.text = "$" + cost;
        cart.SetTotalCost(price);
    }

    IEnumerator DiplayFade()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        bananasMaxed.SetActive(false);
    }

}
