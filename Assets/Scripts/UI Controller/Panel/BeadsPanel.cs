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
        GameManager.Instance.CheckBead += Calculate;
    }

    private void Calculate()
    {
        int i = GameManager.Instance.preTurn - 1;
        haveBalls[i].text = $"X{GameManager.Instance.haveBalls[i]}";
        gainBalls[i].text = $"X{GameManager.Instance.GainBalls[i]}";
    }

    private void OnDisable()
    {
        GameManager.Instance.CheckBead -= Calculate;

    }
}
