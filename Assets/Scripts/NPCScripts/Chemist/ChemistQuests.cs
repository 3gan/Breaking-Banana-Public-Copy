using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChemistQuests : MonoBehaviour
{
    public GameObject MainPage;
    public GameObject InfoPage;
    public GameObject AttemptsPage;
    public GameObject resultsPage;
    public GameObject player;

    
    public GameObject itemDisplay;

    public void OpenMainPage()
    {
        MainPage.SetActive(true);
        player.gameObject.SetActive(false);
    }
    public void CloseMainPage()
    {
        MainPage.SetActive(false);
        
        player.transform.position = new(player.transform.position.x, player.transform.position.y-1, 0);
        player.gameObject.SetActive(true);

        
    }
    public void CloseResultsPage()
    {
        MainPage.SetActive(true);
        resultsPage.SetActive(false);
        resultsPage.transform.GetChild(3).GetComponent<TMP_Text>().text = "";
        resultsPage.transform.GetChild(7).GetComponent<Image>().sprite = itemDisplay.GetComponent<Image>().sprite;
    }
    public void OpenInfoPage()
    {
        InfoPage.SetActive(true);
    }
    public void CloseInfoPage()
    {
        InfoPage.SetActive(false);
    }
    public void OpenAttemptsPage()
    {
        AttemptsPage.SetActive(true);
    }
    public void CloseAttemptsPage()
    {
        AttemptsPage.SetActive(false);
    }

}