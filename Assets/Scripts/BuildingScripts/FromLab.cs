using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromLab : MonoBehaviour
{
    public GameObject player;
    public GameObject TolabLocation;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            player.transform.position = new(TolabLocation.gameObject.transform.position.x, TolabLocation.gameObject.transform.position.y-1, 0);

        }
    }
}
