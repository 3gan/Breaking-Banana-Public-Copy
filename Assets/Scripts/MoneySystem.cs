using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    public int count;
    public TMP_Text text;

    void Start()
    {
        
    }

    public void decrease(int amount)
    {
        count -= amount;
        text.text = "" + count;
    }

    public void increase(int amount)
    {
        count += amount;
        text.text = "" + count;
    }
}