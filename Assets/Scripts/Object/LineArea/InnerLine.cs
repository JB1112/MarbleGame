using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerLine : MonoBehaviour
{
    public GameObject Wall;
    public BoxCollider collider;

    public float time = 3f; //임시 1초 설정

    private void Awake()
    {
        GameManager.Instance.isWaiting = true;
    }

    void Start()
    {
        GameManager.Instance.LoseBead += MakeWall;
        StartCoroutine(EraseWall(time));
        collider.enabled = false;
    }

    private void MakeWall(int obj)
    {
        GameManager.Instance.isWaiting = true;
        Wall.SetActive(true);
        collider.enabled = false;
        StartCoroutine(EraseWall(time));
    }

    IEnumerator EraseWall(float time)
    {
        yield return new WaitForSeconds(time);
        Wall.SetActive(false);
        collider.enabled = true;
        GameManager.Instance.isWaiting = false;
    }

    private void OnDisable()
    {
        GameManager.Instance.LoseBead -= MakeWall;
    }
}
