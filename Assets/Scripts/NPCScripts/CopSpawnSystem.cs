using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopSpawnSystem : MonoBehaviour
{

    public SusSystem susSystem;
    public GameObject cop;
    public GameObject cop1;
    public GameObject cop2;
    public GameObject cop3;
    public GameObject cop4;
    public GameObject cop5;
    public GameObject[] spawns;

    public GameObject copsScreen;
    public GameObject bunkerMenu;
    public GameObject gameOverScreen;
    private float currentSus;
    // Start is called before the first frame update
    void Start()
    {
        currentSus = susSystem.count;
        StartCoroutine(CopsSpawnRate());
    }

    public void Update()
    {
       
        if (bunkerMenu.gameObject.activeSelf)
        {
            copsScreen.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(CopsSpawnRate());
        }

        if (gameOverScreen.gameObject.activeSelf)
        {
            copsScreen.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(CopsSpawnRate());
        }
    }

    public IEnumerator CopsScreen()
    {
        while (true)
        {
            copsScreen.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            copsScreen.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new(1, 0, 0, 0.3f);
            yield return new WaitForSeconds(0.5f);
            copsScreen.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new(0, 0, 1, 0.3f);
        }
       
    }
    IEnumerator CopsSpawnRate()
    {
       
        yield return new WaitForSeconds(300);

        int Spawnchance = Random.Range(0, 99);
        Debug.Log(Spawnchance);
        currentSus = susSystem.count;
        //if spawnchance is less then the current sus spawn cop       
        if (Spawnchance < currentSus)
        {
            StartCoroutine(CopsScreen());
            //spawn 1 cop
            if (currentSus < 15)
            {
                cop.SetActive(true);
                cop.transform.position = spawns[0].transform.position;
            }
            else if (currentSus < 30)
            {
                cop.SetActive(true);
                cop.transform.position = spawns[0].transform.position;
                cop1.SetActive(true);
                cop1.transform.position = spawns[0].transform.position;
            }
            else if(currentSus < 45)
            {
                cop.SetActive(true);
                cop.transform.position = spawns[0].transform.position;
                cop1.SetActive(true);
                cop1.transform.position = spawns[1].transform.position;
                cop2.SetActive(true);
                cop2.transform.position = spawns[2].transform.position;
            }
            else if (currentSus < 60)
            {
                cop.SetActive(true);
                cop.transform.position = spawns[0].transform.position;
                cop1.SetActive(true);
                cop1.transform.position = spawns[0].transform.position;
                cop2.SetActive(true);
                cop2.transform.position = spawns[1].transform.position;
                cop3.SetActive(true);
                cop3.transform.position = spawns[2].transform.position;
            }
            else if (currentSus < 75)
            {
                cop.SetActive(true);
                cop.transform.position = spawns[0].transform.position;
                cop1.SetActive(true);
                cop1.transform.position = spawns[0].transform.position;
                cop2.SetActive(true);
                cop2.transform.position = spawns[1].transform.position;
                cop3.SetActive(true);
                cop3.transform.position = spawns[2].transform.position;
                cop4.SetActive(true);
                cop4.transform.position = spawns[3].transform.position;
            }
            else
            {
                cop.SetActive(true);
                cop.transform.position = spawns[0].transform.position;
                cop1.SetActive(true);
                cop1.transform.position = spawns[0].transform.position;
                cop2.SetActive(true);
                cop2.transform.position = spawns[1].transform.position;
                cop3.SetActive(true);
                cop3.transform.position = spawns[2].transform.position;
                cop4.SetActive(true);
                cop4.transform.position = spawns[3].transform.position;
                cop5.SetActive(true);
                cop5.transform.position = spawns[4].transform.position;
            }

        }

        StartCoroutine(CopsSpawnRate());
    }

    public void hideCops()
    {
        cop.SetActive(false);
        cop.transform.position = new(100, 100, 0);
        cop1.SetActive(false);
        cop1.transform.position = new(100, 100, 0);
        cop2.SetActive(false);
        cop2.transform.position = new(100, 100, 0);
        cop3.SetActive(false);
        cop3.transform.position = new(100, 100, 0);
        cop4.SetActive(false);
        cop4.transform.position = new(100, 100, 0);
        cop5.SetActive(false);
        cop5.transform.position = new(100, 100, 0);
    }
}
