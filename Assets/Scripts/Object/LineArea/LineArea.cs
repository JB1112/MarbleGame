using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineArea : MonoBehaviour
{
    public List<GameObject> lines = new List<GameObject>();
    void Start()
    {
        GameManager.GameStart += DrawLine;
    }

    private void DrawLine()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].SetActive(true);
        }
    }

    private void OnDisable()
    {
        GameManager.GameStart -= DrawLine;
    }
}
