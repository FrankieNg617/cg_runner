using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManage : MonoBehaviour
{
    [SerializeField] Slider timerBar;
    [SerializeField] float gameTime;
    private bool stopTimer;
    private float timer;

    public void startTimer()
    {
        timer = 0f;
        stopTimer = false;
        timerBar.maxValue = gameTime;
        timerBar.value = gameTime;
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        float time = gameTime - timer;
        
        if(time <= 0)
        {
            stopTimer = true;
        }

        if(stopTimer == false)
        {
            timerBar.value = time;
        }
    }
}
