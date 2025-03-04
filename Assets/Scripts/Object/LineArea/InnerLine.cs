using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerLine : MonoBehaviour
{
    public GameObject Wall;
    public BoxCollider collider;

    public float time = 3f; //�ӽ� 1�� ����

    private void Awake()
    {
        GameManager.isWaiting = true;
    }

    void Start()
    {
        GameManager.LoseBead += MakeWall;
        StartCoroutine(EraseWall(time));
        collider.enabled = false;
    }

    private void MakeWall(int obj)
    {
        GameManager.isWaiting = true;
        Wall.SetActive(true);
        collider.enabled = false;
        StartCoroutine(EraseWall(time));
    }

    IEnumerator EraseWall(float time)
    {
        yield return new WaitForSeconds(time);
        Wall.SetActive(false);
        collider.enabled = true;
        GameManager.isWaiting = false;
    }

    private void OnDisable()
    {
        GameManager.LoseBead -= MakeWall;
    }
}
