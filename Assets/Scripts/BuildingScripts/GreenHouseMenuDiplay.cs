using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHouseMenuDiplay : MonoBehaviour
{
    public GameObject menu;
    public GameObject player;
    public GameObject exitButton;

    private Vector3 playerPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            playerPosition = player.transform.position;
            player.SetActive(false);
            menu.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void closeMenu()
    {
        
        player.SetActive(true);
        player.transform.position = new(playerPosition.x, playerPosition.y - 1, 0);
    }

 
}
