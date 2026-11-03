using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLabController : MonoBehaviour
{
    public GameObject player;
    private Vector3 newPos = new(-65.6f, 37.8f, 0);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player")) 
        {
        
                player.transform.position = newPos;
           
        }
    }

}

