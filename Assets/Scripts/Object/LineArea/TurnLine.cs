using System;
using UnityEngine;

public class TurnLine : MonoBehaviour
{
    public GameObject endLine;

    private float distance;

    private void Awake()
    {
        GameManager.Instance.chekDistance += CheckDistance;
        GameManager.Instance.GameStart += LineOff;
    }

    private void LineOff()
    {
        endLine.SetActive(false);

    }

    private void CheckDistance(Vector3 ballDistance)
    {
        distance = MathF.Abs(ballDistance.z - endLine.transform.position.z);

        GameManager.Instance.CheckScore?.Invoke(distance);
    }

    private void OnDisable()
    {
        GameManager.Instance.chekDistance -= CheckDistance;
        GameManager.Instance.GameStart -= LineOff;
    }
}
