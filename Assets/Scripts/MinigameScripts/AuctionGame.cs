using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class AuctionGame : MonoBehaviour
{
    public GameObject auctionGameUI;
    public GameObject resultsUI; // UI for displaying top 3 bidders
    public GameObject player;
    public GameObject tutorialUI;
    public GameObject susUi;
    public MoneySystem moneySystem;
    public GameObject npc;
    public SusSystem susSystem;

    public TMP_Text  titleText, timerText, suspicionText, resultsText, roundsText, auctionParticipantsText, susText;
    public Button[] buyerButtons;
    public Sprite regularBuyerSprite, highBuyerSprite, purpleBuyerSprite, notPurpleBuyerSprite;
    public float baseItemValue = 100; // Base price of the auctioned item
    private InventoryController inventoryController;
    
    private int totalRounds = 5; // Each auction lasts 5 rounds
    private int currentRound;
    private float suspicionLevel = 0f;
    private int auctionParticipants = 0;
    private float difficultyMultiplier;
    private float roundTimeRemaining;
    private List<float> bids = new List<float>(); // Stores bids
    private Vector3 playerExitPosition;

    public float roundDuration = 5f;
    

    void Start()
    {
        auctionGameUI.SetActive(false);
        tutorialUI.SetActive(false);
        resultsUI.SetActive(false);
        //npc.SetActive(false);
        //susUi.SetActive(false);// Invis before the begining of sus activity
        moneySystem = FindFirstObjectByType<MoneySystem>();
        susSystem = FindFirstObjectByType<SusSystem>();
    }

    public void StartAuction()
    {
        tutorialUI.SetActive(true); // Show the tutorial first
        susText.text = "";
    }
    public void CloseTutorialAndStartAuction()
    {
        tutorialUI.SetActive(false); // Hide the tutorial
        BeginAuction(); // Start auction after closing tutorial
    }

    void BeginAuction()
    {
        susUi.SetActive(true);
        playerExitPosition = player.transform.position + new Vector3(0, -2, 0);
        roundTimeRemaining = roundDuration;
        suspicionLevel = 0;
        auctionParticipants = 0;
        difficultyMultiplier = 1f;
        bids.Clear();
        currentRound = totalRounds;
        UpdateRoundText();

        auctionGameUI.SetActive(true);
        StartCoroutine(AuctionTimer());
        RefreshBuyers();
    }


    IEnumerator AuctionTimer()
    {
        while (currentRound > 0)
        {
            roundTimeRemaining = 2f; // Each round lasts 2 seconds

            while (roundTimeRemaining > 0)
            {
                timerText.text = "" + Mathf.Ceil(roundTimeRemaining);
                roundTimeRemaining -= Time.deltaTime;
                yield return null;
            }

            currentRound--; // Reduce the round count **only when time runs out**
            UpdateRoundText();

            if (currentRound <= 0)
            {
                EndAuction();
                yield break;
            }

            RefreshBuyers(); // Get new buyers for the next round
        }
    }



    void RefreshBuyers()
    {
        foreach (Button btn in buyerButtons)
        {
            btn.onClick.RemoveAllListeners(); // Clear previous click events
            btn.interactable = true; // Enable button
            btn.GetComponent<Image>().color = Color.white; // Restore original brightness

            int buyerType = GetRandomBuyerType();
            Image btnImage = btn.GetComponent<Image>();

            if (buyerType == 0)
            {
                btnImage.sprite = regularBuyerSprite;
                btn.onClick.AddListener(() => AcceptBuyer(0));
            }
            else if (buyerType == 1)
            {
                btnImage.sprite = highBuyerSprite;
                btn.onClick.AddListener(() => AcceptBuyer(1));
            }
            else if (buyerType == 2)
            {
                float rand = Random.value;
                float notPurpleChance = 0.1f * difficultyMultiplier; // Disguised Suspicious people
                if (rand < notPurpleChance)
                {
                    btnImage.sprite = notPurpleBuyerSprite;
                }
                else
                {
                    btnImage.sprite = purpleBuyerSprite;
                }
                btn.onClick.AddListener(() => AcceptBuyer(2));
            }
            else
            {
                btnImage.sprite = regularBuyerSprite;
                btn.onClick.AddListener(() => AcceptBuyer(0));
            }
        }
    }

    int GetRandomBuyerType()
    {
        float purpleChance = 0.1f * difficultyMultiplier;
        float highChance = 0.2f;

        float rand = Random.value;
        if (rand < purpleChance) return 2; // Purple Buyer
        if (rand < highChance) return 1;   // High-Paying Buyer
        return 0;                          // Regular Buyer
    }

    public void AcceptBuyer(int buyerType)
    {
        // Get the clicked button
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        if (clickedButton != null)
        {
            clickedButton.interactable = false; // Disable button
            clickedButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f); // Dim the sprite
        }

        float bidAmount = GenerateBid(buyerType);
        bids.Add(bidAmount);

        if (buyerType == 2) // Purple buyer increases suspicion
        {
            suspicionLevel += 10;
        }

        suspicionText.text = "" + suspicionLevel / 10 + "";
        difficultyMultiplier += 0.1f; // Increase difficulty
        auctionParticipants++;
        UpdateParticipantsText();
   
        }


    float GenerateBid(int buyerType)
    {
        float multiplier = 1f;
        int chance = 0;
        switch (buyerType)
        {
            case 0: // Regular Buyer (80%)
                multiplier = Random.Range(0.8f, 1.2f);
                break;
            case 1: // High Buyers (20%)
                chance = Random.Range(1, 4);
                if (chance <= 1) // Higher Buyer (5%)
                {
                    multiplier = Random.Range(1.5f, 5.0f); 
                }
                else if (chance == 2) // Very High Buyer (5%)
                {
                    multiplier = Random.Range(1.5f, 10.0f);
                }
                else // High Buyer (10%)
                {
                    multiplier = Random.Range(1.5f, 2.0f);
                }
                break;
            case 2: // Suspicious Buyer
                multiplier = Random.Range(0.0f, 3.0f);
                break;
        }

        return baseItemValue * multiplier;
    }

    public void EndAuction()
    {
        StopCoroutine(AuctionTimer());
        auctionGameUI.SetActive(false);
        resultsUI.SetActive(true);
        StartCoroutine(DisplayTopBidders());
    }

    IEnumerator DisplayTopBidders()
    {
        if (bids.Count == 0)
        {
            resultsText.text = "No bids were placed!";
            susText.text = ""; // Clear suspicious message
            yield break;
        }

        while (bids.Count < 3) // Ensure we have at least 3 bids
        {
            bids.Add(0);
        }

        var topBidders = bids.OrderByDescending(bid => bid).Take(3).ToList();

        resultsText.text = "";
        suspicionText.text = "";

        float highestBid = topBidders[0];
        float totalEarned = highestBid;

        // Reveal 3rd place
        resultsText.text = $"3. $ {topBidders[2]:F2}";
        yield return new WaitForSeconds(1.5f);

        // Reveal 2nd place (shifting 3rd down)
        resultsText.text = $"2. $ {topBidders[1]:F2}\n3. $ {topBidders[2]:F2}";
        yield return new WaitForSeconds(1.5f);

        // Reveal 1st place (shifting 2nd and 3rd down)
        resultsText.text = $"1. $ {topBidders[0]:F2}\n2. $ {topBidders[1]:F2}\n3. $ {topBidders[2]:F2}";
        yield return new WaitForSeconds(1.5f);

        if (moneySystem != null)
        {
            moneySystem.count += Mathf.RoundToInt(highestBid);
            moneySystem.text.text = moneySystem.count.ToString();
            PlayerPrefs.SetInt("moneyAmount", moneySystem.count);
        }

        if (susSystem != null)
        {
            susSystem.IncreaseSuspicion(Mathf.RoundToInt(suspicionLevel));
        }

        if (suspicionLevel > 0)
        {
            yield return new WaitForSeconds(1.5f);
            susText.text = "A suspicious buyer might have slipped through...";
        }

        if (susSystem != null && susSystem.count >= 99)
        {
            npc.SetActive(true);
        }
    }


    public void CloseTopBidders()
    {
        resultsUI.SetActive(false);
        player.transform.position = playerExitPosition; // Move player outside
        player.SetActive(true); // Show player again
    }
    void UpdateRoundText()
    {
        roundsText.text = "Rnd " + currentRound;
    }
    public void SetBaseItemValue(float value)
    {
        baseItemValue = value;
    }
    void UpdateParticipantsText()
    {
        if (auctionParticipantsText != null)
        {
            auctionParticipantsText.text = "Participants: " + auctionParticipants;
        }
    }
}
