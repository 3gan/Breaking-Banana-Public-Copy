using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerCollider : MonoBehaviour
{
    public GameObject menu;
    public GameObject copsMenu;
    public GameObject player;

    private Vector3 playerPosition;
    private CopSpawnSystem spawnSystem;

    public void Start()
    {
        spawnSystem = FindAnyObjectByType<CopSpawnSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            copsMenu.SetActive(false);
            playerPosition = player.transform.position;
            player.SetActive(false);
            menu.transform.GetChild(0).gameObject.SetActive(true);
            menu.GetComponentInChildren<Bunker>().PlayBunkerScreen();
        }
    }

 
}
