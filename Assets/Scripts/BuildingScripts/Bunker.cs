using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Bunker : MonoBehaviour
{
    public GameObject background;
    public GameObject menu;
    public TMP_Text text;
    public TMP_Text noCopsText;
    private float alpha = 0.01f;
    public GameObject[] Cops;
    public Player player;
    public void PlayBunkerScreen()
    {
        alpha = 0;
        StartCoroutine(Darken());
        StartCoroutine(CloseBunkerScreen());
        
        int copsFound = 0;
        for (int i = 0; i < Cops.Length; i++)
        {
            if (Cops[i].gameObject.activeSelf)
            {
                Cops[i].gameObject.SetActive(false);
                copsFound++;
            }
        }

        if (copsFound == 0)
        {
            noCopsText.gameObject.SetActive(true);
            StartCoroutine(noCops());
        }
    }

    IEnumerator Darken()
    {
        yield return new WaitForSecondsRealtime(0.01f);

        background.GetComponent<Image>().color = new(0,0,0, alpha);
        text.color = new Color(1,1,1, alpha);
        alpha += 0.01f;

        StartCoroutine(Darken());
    }

    IEnumerator noCops()
    {
        yield return new WaitForSecondsRealtime(0.01f);

        noCopsText.color = new Color(noCopsText.color.r, noCopsText.color.g, noCopsText.color.b, alpha);
        alpha += 0.01f;

        StartCoroutine(noCops());
    }
        IEnumerator CloseBunkerScreen()
    {
        yield return new WaitForSecondsRealtime(5);
        
        player.gameObject.SetActive(true);
        player.transform.position = new(player.transform.position.x, player.transform.position.y - 1.5f, 0);

        text.color = new Color(1, 1, 1, 0);
        noCopsText.color = new Color(1, 1, 1, 0);
        background.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        noCopsText.gameObject.SetActive(false);

        menu.SetActive(false);
        StopCoroutine(Darken());
        StopCoroutine(noCops());
        StopCoroutine(CloseBunkerScreen());

    }
}
