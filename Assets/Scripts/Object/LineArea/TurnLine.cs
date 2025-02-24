using System;
using UnityEngine;

public class TurnLine : MonoBehaviour
{
    public GameObject endLine;

    private float distance;

    private void Awake()
    {
        GameManager.chekDistance += CheckDistance;
        GameManager.GameStart += LineOff;
    }

    private void LineOff()
    {
        endLine.SetActive(false);

    }

    private void CheckDistance(Vector3 ballDistance)
    {
        distance = MathF.Abs(ballDistance.z - endLine.transform.position.z);

        Debug.Log(distance);
    }

    private void OnDisable()
    {
        GameManager.chekDistance -= CheckDistance;
        GameManager.GameStart -= LineOff;
    }
}
