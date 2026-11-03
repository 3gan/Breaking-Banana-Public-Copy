using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages; 

    // Start is called before the first frame update
    void Start()
    {
        OpenTab(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenTab(int tabNum)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.gray;
        }

        pages[tabNum].SetActive(true);
        tabImages[tabNum].color = Color.white;

    }
}
