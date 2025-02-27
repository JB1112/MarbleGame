using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeadsPanel : MonoBehaviour
{
    public List<TextMeshProUGUI> haveBalls;
    public List<TextMeshProUGUI> gainBalls;

    void Start()
    {
        GameManager.CheckBead += Calculate;
    }

    private void Calculate()
    {
        int i = GameManager.turn - 2;
        if (i < 0)
        {
            i = 2;
        }
        haveBalls[i].text = $"X{GameManager.haveBalls[i]}";
        gainBalls[i].text = $"X{GameManager.GainBalls[i]}";
    }

    private void OnDisable()
    {
        GameManager.CheckBead -= Calculate;

    }
}
