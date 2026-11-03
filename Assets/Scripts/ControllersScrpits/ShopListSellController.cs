using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopListSellController : MonoBehaviour
{
    public Button addToCartButton;
    public Button notInInventory;
    public GameObject cartItemPrefab; // prefab for item diplay in cart
    public GameObject cart;
    public TMP_Text cartText;
    public int price; // price of item

    private InventoryController inventoryController;

    private void Start()
    {
        inventoryController = FindAnyObjectByType<InventoryController>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && addToCartButton.IsActive() == true)
        {
            AddToCart();
        }
    }
    public void AddToCart()
    {
        int j = 0;
        for (int i = 0; i < cart.transform.GetChild(0).transform.GetChild(0).childCount; i++)
        {
            if (cart.transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).GetComponent<ShopCartItemSellController>().item.GetComponent<Item>().GetItemID()
                 == cartItemPrefab.GetComponent<ShopCartItemSellController>().item.GetComponent<Item>().GetItemID())
            {
                
                cart.transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).GetComponent<ShopCartItemSellController>().IncreaseAmmount();
                j++;
            }
        }

        if (j == 0)
        {
            cart.SetActive(true);
            cartText.text = "";
            cart.GetComponent<CartController>().SetTotalCost(price);
            Instantiate(cartItemPrefab, cart.transform.GetChild(0).transform.GetChild(0).transform);
            
        }
    }
    public void DiplayButton()
    {
        if (inventoryController.GetAmmountOfItemsInInventory(cartItemPrefab.GetComponentInChildren<ShopCartItemSellController>().item.GetComponent<Item>().GetItemID()) == 0)
        {
            notInInventory.gameObject.SetActive(true);
        }
        else
        {
            addToCartButton.gameObject.SetActive(true);
        }

    }
    public void HideButton()
    {
        if (inventoryController.GetAmmountOfItemsInInventory(cartItemPrefab.GetComponentInChildren<ShopCartItemSellController>().item.GetComponent<Item>().GetItemID()) == 0)
        {
            notInInventory.gameObject.SetActive(false);
        }
        else
        {
            addToCartButton.gameObject.SetActive(false);
        }
    }
}
