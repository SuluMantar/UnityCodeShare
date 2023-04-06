using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    public bool inProgress;
    private DateTime TimerStart;
    private DateTime TimerEnd;

    [SerializeField]
    private Text startTime;
    [SerializeField]
    private Text endTime;
    [SerializeField]
    private Text timeLeftText;


    private int Days;
    private int Hours;
    private int Minutes;
    private int Seconds;

    private Coroutine lastTimer;
    private Coroutine lastDisplay;

    // Start TimerToDisplay
    private void InitializeTimer(int days, int hours, int minutes, int seconds)
    {
        startTime.text = "Start Time: \n" + TimerStart;
        endTime.text = "Start Time: \n" + TimerEnd;
        lastDisplay = StartCoroutine(DisplayTime(days, hours, minutes, seconds));
    }


    // Calculate Time between start and end time
    public void StartTimer(int days, int hours, int minutes, int seconds)
    {
        
        TimerStart = DateTime.Now;
        TimeSpan time = new TimeSpan(days, hours, minutes, seconds);
        TimerEnd = TimerStart.Add(time);
        inProgress = true;
        Debug.Log(TimerStart + " " + TimerEnd);

        InitializeTimer(days, hours, minutes, seconds);
        lastTimer = StartCoroutine(Timer());

    }

    // Start Timer
    private IEnumerator Timer()
    {
        DateTime start = DateTime.Now;
        var secondsToFinished = (TimerEnd - start).TotalSeconds;
        yield return new WaitForSeconds(Convert.ToSingle(secondsToFinished));

        inProgress = false;

        Debug.Log("Crafting Finished");

    }



    // Set TimerText 
   /* private IEnumerator DisplayTime(int days, int hours, int minutes, int seconds)
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
                if (timeLeft.days != 0)
                {
                    text += timeLeft.days + "d";
                    text += timeLeft.hours + "h";
                    
                    Debug.Log("Working");
                    yield return new WaitForSeconds(timeLeft.minutes * 60);
                }
                else if (timeLeft.hours != 0)
                {
                    text += timeLeft.hours + "h";
                    text += timeLeft.minutes + "m";
                    Debug.Log("Working: Hours");
                    yield return new WaitForSeconds(timeLeft.seconds);
                }
                else if (timeLeft.Minutes != 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecondsLeft);
                    text += ts.minutes + "m";
                    text += ts.seconds + "s";
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
    */
    private IEnumerator DisplayTime(int days, int hours, int minutes, int seconds)
    {
        this.Days = days;
        this.Hours = hours;
        this.Minutes = minutes;
        this.Seconds = seconds;

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
                else if (timeLeft.Minutes != 0) // Fixed capitalization of Minutes
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecondsLeft);
                    text += ts.Minutes + "m"; // Fixed capitalization of Minutes
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
