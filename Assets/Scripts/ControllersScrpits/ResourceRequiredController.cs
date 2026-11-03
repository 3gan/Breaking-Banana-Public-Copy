using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceRequiredController : MonoBehaviour
{

   
    public TMP_Text amountRequired;
    public GameObject itemText;
    public Item item;
    public int itemAmountRequired;

    private InventoryController inventoryController;
    private void Start()
    {
        inventoryController = FindAnyObjectByType<InventoryController>();
        itemText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        amountRequired.text = "" + inventoryController.GetAmmountOfItemsInInventory(item.GetItemID()) + "/"+ itemAmountRequired;

        if (inventoryController.GetAmmountOfItemsInInventory(item.GetItemID()) <= itemAmountRequired)
        {
            amountRequired.color = Color.red;
        }
        else
        {
            amountRequired.color = Color.green;
        }
    }

    public void DiplayTextTip()
    {
        itemText.gameObject.SetActive(true);
    }
    public void HideTextTip()
    {
        itemText.gameObject.SetActive(false);
    }
}
