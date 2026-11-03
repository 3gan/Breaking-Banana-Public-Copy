using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookAttemptCreator : MonoBehaviour
{
    public Image img1;
    public Image img2;
    public Image img3;

    public GameObject items;

    private ItemDictionary itemDictionary;
    public void Start()
    {
        
    }
    public void setImages(int ID1, int ID2, int ID3)
    {
        itemDictionary = FindAnyObjectByType<ItemDictionary>();
        
        GameObject Obj1 = itemDictionary.GetItemPrefab(ID1);
        GameObject Obj2 = itemDictionary.GetItemPrefab(ID2);
        GameObject Obj3 = itemDictionary.GetItemPrefab(ID3);
        
        img1.GetComponent<Image>().sprite = Obj1.GetComponent<Image>().sprite; 
        img2.GetComponent<Image>().sprite = Obj2.GetComponent<Image>().sprite; 
        img3.GetComponent<Image>().sprite = Obj3.GetComponent<Image>().sprite; 
        
    }
}
