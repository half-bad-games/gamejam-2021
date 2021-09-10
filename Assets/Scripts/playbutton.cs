using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playbutton : MonoBehaviour
{

    public AudioSource EASTEREGG;

    public void PlayGame ()
    {
        SceneManager.LoadScene("PotatoeGame");
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
        SceneManager.LoadScene("ui");

        EASTEREGG.Stop();
    }
    public void Sverri()
    {
        SceneManager.LoadScene("sverri");
    }

    public void Logo()
    {
        SceneManager.LoadScene("concept art");
    }

    public void Jóhan()
    {
        SceneManager.LoadScene("Jóohan");
    }
    public void AUDIO()
    {
        EASTEREGG.Play();
    }

}



