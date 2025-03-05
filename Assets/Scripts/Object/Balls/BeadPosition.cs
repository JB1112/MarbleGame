using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadPosition : MonoBehaviour
{
    void Start()
    {

        GameManager.Instance.spwanPosition.Clear();

        foreach(Transform child in transform)
        {
            GameManager.Instance.spwanPosition.Add(child);
        }
    }
}
