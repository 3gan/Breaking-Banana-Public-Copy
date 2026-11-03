using UnityEngine;
using UnityEngine.UI;

public class ShopBuilding : MonoBehaviour
{
    public GameObject sellingMenu; // Assign the Selling Menu Canvas in Inspector
    public GameObject player;
    public InventoryController inventory;
    public MoneySystem moneySystem;
    private int ID;
    
private Vector3 playerExitPosition;
  

    private void Start()
    {
        sellingMenu.SetActive(false);
        inventory = FindFirstObjectByType<InventoryController>();
        moneySystem = FindFirstObjectByType<MoneySystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerExitPosition = player.transform.position + new Vector3(0, -1, 0); // Move player down when exiting
            OpenSellingMenu();
        }
    }

    public void OpenSellingMenu()
    {
        sellingMenu.SetActive(true);
        player.SetActive(false); // Hide the player
    }

    public void SetID(int num)
    {
        ID = num;
    }
    public void SellBananas(int price)
    {
        int totalBananas = 0;

        foreach (Transform slotTransform in inventory.inventoryPanel.transform)
        {
            Slots slot = slotTransform.GetComponent<Slots>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                if (item.GetItemID() == ID) // Fixed: Compare int with int
                {
                    totalBananas += item.GetAmountInStack();
                    Destroy(slot.currentItem.gameObject); // Remove the item from inventory
                    slot.currentItem = null;
                    slot.itemAmmountDisplay.text = "";
                }
            }
        }

        if (totalBananas > 0)
        {
            int totalPrice = totalBananas * price;
            moneySystem.count += totalPrice;
            moneySystem.text.text = moneySystem.count.ToString();
            Debug.Log($"Sold {totalBananas} bananas for {totalPrice} coins!");
        }
        else
        {
            Debug.Log("No bananas to sell!");
        }
    }

    public void CloseSellingMenu()
    {
        sellingMenu.SetActive(false);
        player.transform.position = playerExitPosition; // Move player outside
        player.SetActive(true); // Show player again
    }
}
