using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadPosition : MonoBehaviour
{
    void Start()
    {

        GameManager.spwanPosition.Clear();

        foreach(Transform child in transform)
        {
            GameManager.spwanPosition.Add(child);
        }
    }
}
