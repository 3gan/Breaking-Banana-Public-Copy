using System.Collections;
using UnityEngine;
using TMPro;

public class SusSystem : MonoBehaviour
{
    public float count = 0;
    public TMP_Text text;
    private float tickRate = 60f; // Default: 1 per 60 seconds under 50 suspicion

    void Start()
    {
        count = Mathf.Clamp(count, 0, 100); // Ensure count is within bounds
        StartCoroutine(TickDownSuspicion());
    }

    IEnumerator TickDownSuspicion()
    {
        while (true)
        {
            UpdateTickRate(); // Adjust tick rate based on suspicion level
            yield return new WaitForSeconds(tickRate);

            if (count > 0)
            {
                count--;
                text.text = "" + count;
            }
        }
    }

    void UpdateTickRate()
    {
        if (count < 50)
        {
            tickRate = 30f; // 1 per half a minute
        }
        else
        {
            float extraTicks = (count - 50) / 10; // Each 10 suspicion above 50 doubles delay
            tickRate = 30f * Mathf.Pow(2, extraTicks);
        }
    }
    public void IncreaseSuspicion(int amount)
    {
        count = Mathf.Clamp(count + amount, 0, 100); // Ensure it stays in range
        UpdateSuspicionUI();
    }

    public void PlantingSusIncrease(int chance, float amount)
    {
        if(Random.Range(0, 100) >= chance)
        {
            count += Random.Range(0, amount);
            UpdateSuspicionUI();
        }
    }



    public void UpdateSuspicionUI()
    {
        if (text != null)
        {
            count = Mathf.Round(count);
            text.text = ""+count;
        }
    }
}
