using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimePanel : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float timeRemaining = 10f;
    private bool isRunning = false;

    private void Start()
    {
        GameManager.turnStart += TimeGoseOn;

    }

    private void TimeGoseOn()
    {
        if (!isRunning)
        {
            StartCoroutine(StartTimer());
        }
    }

    private IEnumerator StartTimer()
    {
        isRunning = true;
        timeRemaining = 10f;

        while (timeRemaining > 0)
        {
            if(GameManager.isMoving)
            {
                isRunning = false;
                yield break;
            }
            timeText.text = timeRemaining.ToString("F1");
            yield return new WaitForSeconds(0.1f);
            timeRemaining -= 0.1f;
        }

        timeText.text = "0.0";
        isRunning = false;

        GameManager.turnChange();
    }
}
