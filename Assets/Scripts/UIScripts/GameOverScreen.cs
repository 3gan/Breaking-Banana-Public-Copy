using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject player;
    public MoneySystem moneySystem;
    public GameObject gameOverScreen;
    public GameObject copsScreen;
    public CopSpawnSystem copSpawnSystem;
    public SusSystem susSystem;

    private InventoryController inventoryController;
    public void Start()
    {
        inventoryController = FindAnyObjectByType<InventoryController>();
    }

    public void restartGame()
    {
        moneySystem.count = moneySystem.count / 2;
        moneySystem.text.text = moneySystem.count.ToString();

        susSystem.count = susSystem.count / 2;
        susSystem.UpdateSuspicionUI();

        for (int i = 1; i < inventoryController.itemPrefabs.Length+1; i++)
        {
            inventoryController.RemoveItemsFromInventory(i, Random.Range(0, inventoryController.GetAmmountOfItemsInInventory(i)));
        }

        copSpawnSystem.hideCops();
        player.SetActive(true);
        player.transform.position = new(-25.5f, 35.8f, 0f);
        copsScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }
    public void returnToMenu()
    {
        copSpawnSystem.hideCops();

        player.SetActive(true);
        copsScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
