using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GreenHouseController : MonoBehaviour
{
    public Slots placeSlot;
    public Slots pullSlot;
    public Image arrow;
    public TMP_Text info;
    public int timeToGrow;
    private bool growing = false;
    private GreenHouseDictionary greenHouseDictionary;
    private int curentPlaceIndex;
    private int curentPullIndex;
    private int curentIndex;

    public GameObject ghCollider;
    // Start is called before the first frame update
    void Start()
    {
        greenHouseDictionary = FindAnyObjectByType<GreenHouseDictionary>();
    }

   
    // Update is called once per frame
    void Update()
    {
        if (placeSlot.currentItem != null)
        {
            StartGrowth();

        }
        else
        {
            info.text = "PLACE IN PLANTING BANANAS TO GROW";
            growing = false;
            StopCoroutine(BanananGenerator());
            StopCoroutine(FillArrow());
        }
    }

    public void StartGrowth()
    {
            if (pullSlot.currentItem == null || GetPlaceIndex() == GetPullIndex()) 
            {
                placeSlot.currentItem.transform.localScale = new(2f, 2f, 0f);
                if (placeSlot.currentItem.GetComponent<Item>().isPlantingBananan)
                {
                    if (growing == false)
                    {
                        growing = true;
                        StopCoroutine(BanananGenerator());
                        StartCoroutine(BanananGenerator());

                    }
                    StartCoroutine(FillArrow());
                    info.text = "GROWING ...";
                }
                else
                {
                    info.text = "Must Use Planting Bananan";
                }
            }
            else
            {
                info.text = "TAKE BANANAS BEFOR PLANTING";
                growing = false;
                StopCoroutine(BanananGenerator());
                StopCoroutine(FillArrow());
            }
       
    }
    IEnumerator BanananGenerator()
    {
       
        yield return new WaitForSecondsRealtime(timeToGrow * placeSlot.currentItem.GetComponent<Item>().growthMultplier);

        if(placeSlot.currentItem != null)
        {
            placeSlot.currentItem.GetComponent<Item>().amountInStack--;

            if (pullSlot.currentItem == null)
            {

                Item placeItem = placeSlot.currentItem.GetComponent<Item>();

                for (int i = 0; i < greenHouseDictionary.Bananas.Length; i++)
                {
                    if (greenHouseDictionary.PlantingBananas[i].GetItemID() == placeItem.GetItemID())
                    {
                        GameObject newItem = Instantiate(greenHouseDictionary.Bananas[i].gameObject, pullSlot.transform);
                        newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        newItem.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        pullSlot.currentItem = newItem;

                        Item pullItem = pullSlot.currentItem.GetComponent<Item>();

                        pullSlot.itemAmmountDisplay.text = "" + pullItem.amountInStack;
                        pullSlot.currentItem.transform.localScale = new(2f, 2f, 0f);
                        curentIndex = i;
                        break;
                    }
                }
            }
            else
            {
                Item pullItem = pullSlot.currentItem.GetComponent<Item>();
                if (pullItem.amountInStack < pullItem.maxStackAmount)
                {
                    pullItem.amountInStack++;
                    pullSlot.itemAmmountDisplay.text = "" + pullItem.amountInStack;
                }

            }



            if (placeSlot.currentItem.GetComponent<Item>().GetAmountInStack() <= 0)
            {
                placeSlot.itemAmmountDisplay.text = "" + placeSlot.currentItem.GetComponent<Item>().amountInStack.ToString();
                Destroy(placeSlot.transform.GetChild(1).gameObject);
                info.text = "PLACE IN PLANTING BANANAS TO GROW";
                growing = false;
                arrow.GetComponent<Image>().fillAmount = 0;
                StopCoroutine(BanananGenerator());
                StopCoroutine(FillArrow());

            }
            else
            {
                placeSlot.itemAmmountDisplay.text = placeSlot.currentItem.GetComponent<Item>().amountInStack.ToString();
                growing = false;
                StartGrowth();
            }

        }
        else{
            Debug.Log("NOTGROWING");
        }


    }

    public int GetPlaceIndex()
    {
        Item placeItem = placeSlot.currentItem.GetComponent<Item>();

        for (int i = 0; i < greenHouseDictionary.Bananas.Length; i++)
        {
            if (greenHouseDictionary.PlantingBananas[i].GetItemID() == placeItem.GetItemID())
            {
                curentPlaceIndex = i;
                break;
            }
        }

        return curentPlaceIndex;
    }
    public int GetPullIndex()
    {
        Item pullItem = pullSlot.currentItem.GetComponent<Item>();

        for (int i = 0; i < greenHouseDictionary.Bananas.Length; i++)
        {
            if (greenHouseDictionary.Bananas[i].GetItemID() == pullItem.GetItemID())
            {
                curentPullIndex = i;
                break;
            }
        }

        return curentPullIndex;
    }
    IEnumerator FillArrow()
    {
        arrow.GetComponent<Image>().fillAmount += 0.001f;
        if (arrow.GetComponent<Image>().fillAmount == 1)
        {
            arrow.GetComponent<Image>().fillAmount = 0;
        }
        yield return new WaitForSecondsRealtime(timeToGrow * placeSlot.currentItem.GetComponent<Item>().growthMultplier);
       
    }

    public void closeMenu()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        ghCollider.GetComponent<GreenHouseMenuDiplay>().closeMenu();
    }
}
