using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageShed : MonoBehaviour
{

    public GameObject player;
    private Vector3 playerExitPosition;
    public GameObject menu;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            player = collision.gameObject;
            player.SetActive(false);
            playerExitPosition = player.transform.position + new Vector3(0, -1f, 0); // Move player down when exiting
            menu.SetActive(true);
        }
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        player.transform.position = playerExitPosition; // Move player outside
        player.SetActive(true); // Show player again
    }
}
