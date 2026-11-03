using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AuctionCartItemController : MonoBehaviour
{
    public int amount = 1; //the amount of items being purchased
    public int cost; //the total cost of the item being purchased
    private CartController cart;
    private InventoryController inventoryController;

    public int price; //the price of the individual item being purchased
    public TMP_Text amountText;
    public TMP_Text costText;
    public GameObject item;// the item prefab being purchased
    public GameObject BananasMaxed;
    
    private void Start()
    {
        cart = FindAnyObjectByType<CartController>();
        inventoryController = FindAnyObjectByType<InventoryController>();
        BananasMaxed.SetActive(false);
        price = item.GetComponent<Item>().price;
    }

   
    public void IncreaseBananaAmmount()
    {
        int bananasInInventory = inventoryController.GetAmmountOfItemsInInventory(item.GetComponent<Item>().GetItemID());
        amount++;
        if (amount <= bananasInInventory)
        {
            cost = price * amount;
            amountText.text = "" + amount;
            costText.text = "$" + cost;
            cart.SetTotalCost(price);
        }
        else
        {
            amount--;
            BananasMaxed.SetActive(true);
            StartCoroutine(DiplayFade());
        }

    }
    public void DecreaseBananaAmmount()
    {
        amount--;
        if (amount <= 0)
        {
            Destroy(gameObject);
            cart.SetTotalCost(-price);
            if (cart.GetTotalCost() == 0)
            {
                cart.SetCartText("No Items up for auction");
            }
        }
        else
        {
            cost = price * amount;
            amountText.text = "" + amount;
            costText.text = "$" + cost;
            cart.SetTotalCost(-price);
        }

    }

    IEnumerator DiplayFade()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        BananasMaxed.SetActive(false);
    }
    
}
