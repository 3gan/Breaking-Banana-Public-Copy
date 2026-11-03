using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuctionListController : MonoBehaviour
{
    public Button addToAuctionButton;
    public GameObject notInInventory;
    public GameObject AuctionItemPrefab; // prefab for item diplay in cart
    public GameObject cart;
    public TMP_Text AuctionText;
    public int price; // price of item
    private InventoryController inventoryController;
    private void Start()
    {
        inventoryController = FindAnyObjectByType<InventoryController>();
        price = AuctionItemPrefab.GetComponent<AuctionCartItemController>().item.GetComponent<Item>().price;

    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && addToAuctionButton.IsActive() == true)
        {
            AddToCart();
        }
    }
    public void AddToCart()
    {

        int j = 0;
        for(int i = 0; i < cart.transform.GetChild(0).transform.GetChild(0).childCount; i++)
        {
           if (cart.transform.GetChild(0).transform.GetChild(0).GetChild(i).GetComponent<AuctionCartItemController>().item.GetComponent<Item>().GetItemID()
                == AuctionItemPrefab.GetComponent<AuctionCartItemController>().item.GetComponent<Item>().GetItemID())
            {
                cart.transform.GetChild(0).transform.GetChild(0).GetChild(i).GetComponent<AuctionCartItemController>().IncreaseBananaAmmount();
                j++;
            }
        }
        
        if(j == 0)
        {
            cart.SetActive(true);
            AuctionText.text = "";
            cart.GetComponent<CartController>().SetTotalCost(price);
            Instantiate(AuctionItemPrefab, cart.transform.GetChild(0).transform.GetChild(0).transform);
        }
    }
    public void DiplayButton()
    {
        if (inventoryController.GetAmmountOfItemsInInventory(AuctionItemPrefab.GetComponentInChildren<AuctionCartItemController>().item.GetComponent<Item>().GetItemID()) == 0)
        {
            notInInventory.SetActive(true);
        }
        else
        {
            addToAuctionButton.gameObject.SetActive(true);
        }
       
    }
    public void HideButton()
    {
        if (inventoryController.GetAmmountOfItemsInInventory(AuctionItemPrefab.GetComponentInChildren<AuctionCartItemController>().item.GetComponent<Item>().GetItemID()) == 0)
        {
            notInInventory.SetActive(false);
        }
        else
        {
            addToAuctionButton.gameObject.SetActive(false);
        }
    }
}
