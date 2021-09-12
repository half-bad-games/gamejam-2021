using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Game3");
    }

    public void Exit()
    {
        Application.Quit();
    }


    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Back()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}