using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerLine : MonoBehaviour
{
    public GameObject Wall;
    public BoxCollider collider;

    public float time = 3f; //임시 1초 설정

    void Start()
    {
        GameManager.LoseBead += MakeWall;

        StartCoroutine(EraseWall(time));
        //collider = GetComponent<BoxCollider>();
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
}
