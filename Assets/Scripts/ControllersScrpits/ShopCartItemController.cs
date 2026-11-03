using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCartItemController : MonoBehaviour
{
    public int amount = 1; //the amount of items being purchased
    public int cost; //the total cost of the item being purchased
    private CartController cart;

    public int price; //the price of the individual item being purchased
    public TMP_Text amountText;
    public TMP_Text costText;
    public GameObject item;// the item prefab being purchased
    private void Start()
    {
        cart = FindAnyObjectByType<CartController>();
    }
    public void IncreaseAmmount()
    {
        amount++;
        cost = price * amount;
        amountText.text = "x" + amount;
        costText.text = "$" + cost;
        cart.SetTotalCost(price);
    }
    public void DecreaseAmmount()
    {
        amount--;
        if (amount <= 0)
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

}