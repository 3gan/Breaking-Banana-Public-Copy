using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChemistCooking : MonoBehaviour
{
    public Slots slot1;
    public Slots slot2;
    public GameObject cookAttemptCreator;
    public GameObject attemptsContent;
    public GameObject BeginButton;
    public TMP_Text title;

    public GameObject resultsPanel;

    private int attempts;
    private ItemDictionary ItemDictionary;
    private MoneySystem moneySystem;
    private void Start()
    {
        ItemDictionary = FindAnyObjectByType<ItemDictionary>();
        moneySystem = FindAnyObjectByType<MoneySystem>();
    }

    public void Update()
    {
        if(slot1.currentItem != null)
        {
            slot1.currentItem.transform.localScale = new(2f, 2f, 0f);
        }

        if (slot2.currentItem != null)
        {
            slot2.currentItem.transform.localScale = new(2f, 2f, 0f);
        }
    }
    public void cook()
    {
        if(slot1.currentItem != null && slot2.currentItem != null)
        {
            if(slot1.GetCurrentItem().isPlantingBananan && slot2.GetCurrentItem().isPlantingBananan)
            {
                int ResultID = CheckCombination(slot1.GetCurrentItem().GetItemID(), slot2.GetCurrentItem().GetItemID());

                if (moneySystem.count >= 1000)
                {

                    moneySystem.decrease(1000);
                    Instantiate(ItemDictionary.GetItemPrefab(ResultID), new(-63.5f, 39.5f), Quaternion.identity); //create item
                    Instantiate(cookAttemptCreator, attemptsContent.transform); // create attempt display

                    attemptsContent.transform.GetChild(attempts).GetComponent<CookAttemptCreator>().setImages(slot1.GetCurrentItem().GetItemID(), slot2.GetCurrentItem().GetItemID(), ResultID);
                    attempts++;

                    Item item1 = slot1.currentItem.GetComponent<Item>();
                    Item item2 = slot2.currentItem.GetComponent<Item>();

                    if (item1.GetAmountInStack() <= 1)
                    {
                        Destroy(item1.gameObject);
                        slot1.GetComponent<Slots>().itemAmmountDisplay.text = "" + 0;
                    }
                    else
                    {
                        item1.amountInStack--;
                        slot1.GetComponent<Slots>().itemAmmountDisplay.text = "" + item1.amountInStack;
                    }

                    if (item2.GetAmountInStack() <= 1)
                    {
                        Destroy(item2.gameObject);
                        slot2.GetComponent<Slots>().itemAmmountDisplay.text = "" + 0;
                    }
                    else
                    {
                        item2.amountInStack--;
                        slot2.GetComponent<Slots>().itemAmmountDisplay.text = "" + item2.amountInStack;
                    }
                    StartCoroutine(startResults(ResultID));
                }
                else
                {
                    BeginButton.GetComponent<Image>().color = Color.red;
                    title.text = "No cash, no cook";
                    StartCoroutine(Wait());

                }
            }
            else
            {
                BeginButton.GetComponent<Image>().color = Color.red;
                title.text = "Wrong Bananas";
                StartCoroutine(Wait());
                
            }

        }
        else
        {
            BeginButton.GetComponent<Image>().color = Color.red;
            title.text = "need two bananas";
            StartCoroutine(Wait());
            


        }
    }
 
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        BeginButton.GetComponent<Image>().color = Color.white;
        title.text = "Begin next cook";
    }
    public IEnumerator startResults(int ID)
    {
        
        resultsPanel.SetActive(true);
        resultsPanel.transform.GetChild(6).gameObject.SetActive(false);
        StartCoroutine(CookingDots());
        yield return new WaitForSeconds(5);
        StopAllCoroutines();
        StartCoroutine(Displayresults(ID));
        
     
    }
    public IEnumerator Displayresults(int ID)
    {
        GameObject Obj1 = ItemDictionary.GetItemPrefab(ID);
        resultsPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = "Results";
        yield return new WaitForSeconds(1);
        resultsPanel.transform.GetChild(3).GetComponent<TMP_Text>().text = Obj1.GetComponent<Item>().Name;
        yield return new WaitForSeconds(1);
        resultsPanel.transform.GetChild(7).GetComponent<Image>().sprite = Obj1.GetComponent<Image>().sprite;
        resultsPanel.transform.GetChild(6).gameObject.SetActive(true);
    }

    public IEnumerator CookingDots()
    {
        resultsPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = "Cooking";
        yield return new WaitForSeconds(0.3f);
        resultsPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = "Cooking.";
        yield return new WaitForSeconds(0.3f);
        resultsPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = "Cooking..";
        yield return new WaitForSeconds(0.3f);
        resultsPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = "Cooking...";
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(CookingDots());


    }
    public int CheckCombination(int ID1, int ID2)
    {
        //weed Banana
        if((ID1 == 37 && ID2 == 29) || (ID1 == 29 && ID2 == 37))
        {
            return 47;
        }

        if ((ID1 == 49 && ID2 == 6) || (ID1 == 6 && ID2 == 49))
        {
            return 47;
        }

        //Beer Banana
        if ((ID1 == 29 && ID2 == 49) || (ID1 == 49 && ID2 == 29))
        {
            return 25;
        }

        //RazorBlade Banana
        if ((ID1 == 41 && ID2 == 6) || (ID1 == 6 && ID2 == 41))
        {
            return 39;
        }

        if ((ID1 == 41 && ID2 == 20) || (ID1 == 20 && ID2 == 41))
        {
            return 39;
        }

        //Blodd Banana
        if ((ID1 == 39 && ID2 == 25) || (ID1 == 25 && ID2 == 39))
        {
            return 27;
        }

        //Fent Banana
        if ((ID1 == 27 && ID2 == 29) || (ID1 == 29 && ID2 == 27))
        {
            return 15;
        }

        //TNT Banana
        if ((ID1 == 25 && ID2 == 41) || (ID1 == 41 && ID2 == 25))
        {
            return 21;
        }

        if ((ID1 == 25 && ID2 == 43) || (ID1 == 43 && ID2 == 25))
        {
            return 21;
        }

        //LDS Banana
        if ((ID1 == 15 && ID2 == 47) || (ID1 == 47 && ID2 == 15))
        {
            return 17;
        }

        if ((ID1 == 15 && ID2 == 25) || (ID1 == 25 && ID2 == 15))
        {
            return 17;
        }

        //Acid Banana
        if ((ID1 == 17 && ID2 == 25) || (ID1 == 25 && ID2 == 17))
        {
            return 23;
        }

        if ((ID1 == 17 && ID2 == 15) || (ID1 == 15 && ID2 == 17))
        {
            return 23;
        }

        //Crystal Meth Banana
        if ((ID1 == 23 && ID2 == 15) || (ID1 == 15 && ID2 == 23))
        {
            return 16;
        }

        if ((ID1 == 23 && ID2 == 17) || (ID1 == 17 && ID2 == 23))
        {
            return 16;
        }

        //Grenade Banana
        if ((ID1 == 21 && ID2 == 41) || (ID1 == 41 && ID2 == 21))
        {
            return 31;
        }

        if ((ID1 == 21 && ID2 == 23) || (ID1 == 23 && ID2 == 21))
        {
            return 31;
        }

        //Toxic Waste Banana
        if ((ID1 == 17 && ID2 == 37) || (ID1 == 37 && ID2 == 17))
        {
            return 45;
        }

        if ((ID1 == 23 && ID2 == 41) || (ID1 == 41 && ID2 == 23))    
        {
            return 45;
        }

        //Cocaine Banana
        if ((ID1 == 45 && ID2 == 16) || (ID1 == 16 && ID2 == 45))
        {
            return 14;
        }

        if ((ID1 == 45 && ID2 == 17) || (ID1 == 17 && ID2 == 45))
        {
            return 14;
        }

        //Plutomine Banana
        if ((ID1 == 45 && ID2 == 29) || (ID1 == 29 && ID2 == 45))
        {
            return 35;
        }

        //Nuke Banana
        if ((ID1 == 21 && ID2 == 31) || (ID1 == 31 && ID2 == 21))
        {
            return 33;
        }

        if ((ID1 == 35 && ID2 == 41) || (ID1 == 41 && ID2 == 35))
        {
            return 14;
        }

        //Black Banana
        if ((ID1 == 35 && ID2 == 6) || (ID1 == 6 && ID2 == 45))
        {
            return 14;
        }

        //If same banana given
        if(ID1 == ID2)
        {
            return ID1;
        }

        return 37;
    }

    IEnumerator CookTime()
    {
        yield return new WaitForSecondsRealtime(600);
    }
}
