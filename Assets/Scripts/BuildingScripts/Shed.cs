using UnityEngine;
using UnityEngine.UI;

public class Shed : MonoBehaviour
{
    public GameObject auctionMenuUI;  // Assign the Auction Menu Canvas in Inspector
    public GameObject auctionGameUI;  // Full-screen auction mini-game UI
    public GameObject player;
    public AuctionGame auctionGame;
    public InventoryController inventory;
    private Vector3 playerExitPosition;

    private float auctionBasePrice = 0f; // Store the received price
    private void Start()
    {
        inventory = FindFirstObjectByType<InventoryController>();
        auctionMenuUI.SetActive(false);
        auctionGameUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerExitPosition = player.transform.position + new Vector3(0, -1, 0); // Move player down when exiting
            OpenAuctionMenu();
        }
    }

    public void OpenAuctionMenu()
    {
        auctionMenuUI.SetActive(true);
        player.SetActive(false); // Hide the player
    }


    public void StartAuction()
    {
        if (auctionGame != null)
        {
            auctionMenuUI.SetActive(false );
            auctionGame.SetBaseItemValue(auctionBasePrice);
            auctionGame.StartAuction();
        }
    }

    public void CloseAuctionMenu()
    {
        auctionMenuUI.SetActive(false);
        player.transform.position = playerExitPosition; // Move player outside
        player.SetActive(true); // Show player again
    }
    
    public void ReceiveAuctionPrice(float price)
    {
        auctionBasePrice = price;
    }

}

