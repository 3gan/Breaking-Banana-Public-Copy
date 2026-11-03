using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject SecondCamera;
    public GameObject player;
    //public Player player;

    private int mode = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mode == 0)
            {
                player.GetComponent<PlayerInput>().enabled = false;
                SecondCamera.SetActive(true);
                MainCamera.SetActive(false);
                mode++;
            }
            else
            {
                player.GetComponent<PlayerInput>().enabled = true;
                MainCamera.SetActive(true);
                SecondCamera.SetActive(false);
                mode--;
            }
           
        }
    }
}
