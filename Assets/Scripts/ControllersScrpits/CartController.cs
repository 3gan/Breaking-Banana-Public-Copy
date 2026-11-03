using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CartController : MonoBehaviour
{
    public TMP_Text cartText;
    public TMP_Text totalCostText;
    public GameObject cartContent;
    public GameObject money;
    private InventoryController inventoryController;
    private GameObject cartItem;

    void Start()
    {
        inventoryController = FindAnyObjectByType<InventoryController>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearCart();
        }
    }
    public void StartAuction()
    {
        int totalBananaValue = 0;

        for (int i = 0; i < cartContent.transform.childCount; i++)
        {
            totalBananaValue = cartContent.transform.GetChild(i).GetComponent<AuctionCartItemController>().cost;

            if (cartContent.transform.GetChild(i).GetComponent<AuctionCartItemController>().amount
                <= inventoryController.GetAmmountOfItemsInInventory(cartContent.transform.GetChild(i).GetComponent<AuctionCartItemController>().item.GetComponent<Item>().GetItemID()))
            {
                Destroy(cartContent.transform.GetChild(i).gameObject);
                inventoryController.RemoveItemsFromInventory(cartContent.transform.GetChild(i).GetComponent<AuctionCartItemController>().item.GetComponent<Item>().GetItemID(), cartContent.transform.GetChild(i).GetComponent<AuctionCartItemController>().amount);
            }
            else
            {
            }
        }
        if (totalBananaValue > 0)
        {
            Shed shed = FindFirstObjectByType<Shed>();
            if (shed != null)
            {
                shed.ReceiveAuctionPrice(totalBananaValue);
                shed.StartAuction();
            }
        }
        else
        {
            //Debug.Log("No special bananas to auction!");
        }
    }

    public void Pay()
    {
        if(int.Parse(totalCostText.text) <= money.GetComponent<MoneySystem>().count)
        {
            for (int i = 0; i < cartContent.transform.childCount; i++)
            {
                int price = cartContent.transform.GetChild(i).GetComponent<ShopCartItemController>().price;
                for (int j = 0; j < cartContent.transform.GetChild(i).GetComponent<ShopCartItemController>().amount; j++)
                {
                    cartContent.transform.GetChild(i).GetComponent<ShopCartItemController>().item.GetComponent<Item>().amountInStack = 1;
                    if (money.GetComponent<MoneySystem>().count >= price)
                    {
                        cartItem = cartContent.transform.GetChild(i).GetComponent<ShopCartItemController>().item;
                        bool itemAdded = inventoryController.AddItem(cartItem);
                        if (itemAdded)
                        {

                            totalCostText.text = "0"; 
                            money.GetComponent<MoneySystem>().decrease(price);
                            Destroy(cartContent.transform.GetChild(i).gameObject);
                        }
                        else
                        {
                            //Debug.Log("Inventory Full");
                        }
                    }
                    else
                    {
                        //Debug.Log("Not enough money");
                    }
                }
            }
        }
        
    }

    public void SellItem()
    {
        for (int i = 0; i < cartContent.transform.childCount; i++)
        {
            int price1 = cartContent.transform.GetChild(i).GetComponent<ShopCartItemSellController>().cost;

            if(cartContent.transform.GetChild(i).GetComponent<ShopCartItemSellController>().amount 
                <= inventoryController.GetAmmountOfItemsInInventory(cartContent.transform.GetChild(i).GetComponent<ShopCartItemSellController>().item.GetComponent<Item>().GetItemID()))
            {
               
                SetTotalCost(-price1);
                money.GetComponent<MoneySystem>().increase(price1);
                Destroy(cartContent.transform.GetChild(i).gameObject);
                inventoryController.RemoveItemsFromInventory(cartContent.transform.GetChild(i).GetComponent<ShopCartItemSellController>().item.GetComponent<Item>().GetItemID(), cartContent.transform.GetChild(i).GetComponent<ShopCartItemSellController>().amount);
            }
            else { 
            }
        }
    }

    public void ClearCart()
    {
        for (int i = 0; i < cartContent.transform.childCount; i++)
        {
            totalCostText.text = "" + 0;
            Destroy(cartContent.transform.GetChild(i).gameObject);
        }
    }

    public void SetCartText(string text)
    {
        cartText.text = text;
    }

    public void SetTotalCost(int cost)
    {
        int totalCost = int.Parse(totalCostText.text);
        totalCostText.text = "" + (totalCost + cost);
    }

    public int GetTotalCost()
    {
        return int.Parse(totalCostText.text);
    }
}
