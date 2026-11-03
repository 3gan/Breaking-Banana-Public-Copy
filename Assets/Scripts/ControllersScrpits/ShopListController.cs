using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopListController : MonoBehaviour
{
    public Button addToCartButton;
   
    public GameObject cartItemPrefab; // prefab for item diplay in cart
    public GameObject cart;
    public TMP_Text cartText;
    public int price; // price of item

    private InventoryController inventoryController;

    private void Start()
    {
        inventoryController = FindAnyObjectByType<InventoryController>();
    }
    public void AddToCart()
    {
        int j = 0;
        for (int i = 0; i < cart.transform.GetChild(0).transform.GetChild(0).childCount; i++)
        {
            if (cart.transform.GetChild(0).transform.GetChild(0).GetChild(i).GetComponent<ShopCartItemController>().item.GetComponent<Item>().GetItemID()
                 == cartItemPrefab.GetComponent<ShopCartItemController>().item.GetComponent<Item>().GetItemID())
            {
                cart.transform.GetChild(0).transform.GetChild(0).GetChild(i).GetComponent<ShopCartItemController>().IncreaseAmmount();
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
        addToCartButton.gameObject.SetActive(true);
    }
    public void HideButton()
    {
         addToCartButton.gameObject.SetActive(false);
    }
}
