using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Controls()
    {
        SceneManager.LoadScene("ControlsMenu");
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
