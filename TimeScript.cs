using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    private bool inProgress;
    private DateTime TimerStart;
    private DateTime TimerEnd;

    [SerializeField]
    private Text startTime;
    [SerializeField]
    private Text endTime;
    [SerializeField]
    private Text timeLeftText;


    public int Days;
    public int Hours;
    public int Minutes;
    public int Seconds;

    private Coroutine lastTimer;
    private Coroutine lastDisplay;


    private void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            StartTimer();
        }

    }



    // Start TimerToDisplay
    private void InitializeTimer()
    {
        startTime.text = "Start Time: \n" + TimerStart;
        endTime.text = "Start Time: \n" + TimerEnd;
        lastDisplay = StartCoroutine(DisplayTime());
    }


    // Calculate Time between start and end time
    private void StartTimer()
    {
        TimerStart = DateTime.Now;
        TimeSpan time = new TimeSpan(Days, Hours, Minutes, Seconds);
        TimerEnd = TimerStart.Add(time);
        inProgress = true;
        Debug.Log(TimerStart + " " + TimerEnd);

        InitializeTimer();
        lastTimer = StartCoroutine(Timer());

    }

    // Start Timer
    private IEnumerator Timer()
    {
        DateTime start = DateTime.Now;
        var secondsToFinished = (TimerEnd - start).TotalSeconds;
        yield return new WaitForSeconds(Convert.ToSingle(secondsToFinished));

        inProgress = false;

        Debug.Log("Finished");

    }



    // Set TimerText 
    private IEnumerator DisplayTime()
    {

        DateTime start = DateTime.Now;
        TimeSpan timeLeft = TimerEnd - start;
        var totalSecondsLeft = timeLeft.TotalSeconds;
        var totalSeconds = (TimerEnd - TimerStart).TotalSeconds;
        string text;

        while (inProgress)
        {
            text = "";

            if (totalSecondsLeft > 1)
            {
                if (timeLeft.Days != 0)
                {
                    text += timeLeft.Days + "d";
                    text += timeLeft.Hours + "h";
                    
                    Debug.Log("Working");
                    yield return new WaitForSeconds(timeLeft.Minutes * 60);
                }
                else if (timeLeft.Hours != 0)
                {
                    text += timeLeft.Hours + "h";
                    text += timeLeft.Minutes + "m";
                    Debug.Log("Working: Hours");
                    yield return new WaitForSeconds(timeLeft.Seconds);
                }
                else if (timeLeft.Minutes != 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecondsLeft);
                    text += ts.Minutes + "m";
                    text += ts.Seconds + "s";
                    Debug.Log("Working: Minutes");
                }
                else
                {
                    text += Mathf.FloorToInt((float)totalSecondsLeft) + "s";
                    Debug.Log("Working: Seconds");
                }

                timeLeftText.text = text;
                totalSecondsLeft -= Time.deltaTime;
                Debug.Log("Total seconds left: " + totalSecondsLeft);
                yield return null;
            }
            else
            {
                timeLeftText.text = "Finished";
                inProgress = false;
                break;
            }
            
        }

        yield return null;
    }


    public void SkipTime()
    {
        TimerEnd = DateTime.Now;
        inProgress = false;
        StopCoroutine(lastTimer);

        timeLeftText.text = "Finished";
        StopCoroutine(lastDisplay);


    }

}
