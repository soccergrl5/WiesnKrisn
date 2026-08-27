using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Timer = System.Timers.Timer;

public class TimerScript : MonoBehaviour
{
    public float totalTime = 90f;
    [SerializeField] private TMP_Text timerText;

    void Start()
    {
        timerText.text = "00:00:00";

    }
    void Update()
    {
        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
            
            var minutes = Mathf.FloorToInt(totalTime / 60);
            var seconds = Mathf.FloorToInt(totalTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = "Time's up";
            totalTime = 0;
            DetermineWinnerScript.DetermineWinner();
        }
    }
}
