using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer_Script : MonoBehaviour
{
    public float secondsCount;
    public int minuteCount;
    public float ui_secondsCount;
    public int ui_minuteCount;
    private float secondsCountSaved;
    private int minuteCountSaved;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("timerText").GetComponent<UnityEngine.UI.Text>().text = "0m:" + "0s";
        secondsCount = 0.0f;
        minuteCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("timer");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }



        DontDestroyOnLoad(this.gameObject);


        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ui"))
        {
            if (getSecoundCountSaved() > 0)
                GameObject.Find("timerText").GetComponent<UnityEngine.UI.Text>().text = getMinuteCountSaved() + "m:" + (int)getSecoundCountSaved() + "s";
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("potatoeGame"))
        {
            ui_secondsCount = secondsCount;
            ui_minuteCount = minuteCount;

            //set timer UI
            secondsCount += Time.deltaTime;
            GameObject.Find("timerText").GetComponent<UnityEngine.UI.Text>().text = getMinuteCount() + "m:" + (int)getSecoundCount() + "s";
            if (secondsCount >= 60)
            {
                minuteCount++;
                secondsCount = 0;
            }
        }
    }

    public float getSecoundCount() { return secondsCount; }
    public void setSecoundCount(float aSecondsCount) { secondsCount = aSecondsCount; }
    public int getMinuteCount() { return minuteCount; }
    public void setMinuteCount(int aMinuteCount) { minuteCount = aMinuteCount; }

    public float getSecoundCountSaved() { return secondsCountSaved; }
    public void setSecoundCountSaved(float aSecondsCountSaved) { secondsCountSaved = aSecondsCountSaved; }
    public int getMinuteCountSaved() { return minuteCountSaved; }
    public void setMinuteCountSaved(int aMinuteCountSaved) { minuteCountSaved = aMinuteCountSaved; }
}
