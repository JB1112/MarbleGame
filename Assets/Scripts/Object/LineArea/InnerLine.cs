using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerLine : MonoBehaviour
{
    public GameObject Wall;

    public float time = 2f; //�ӽ� 1�� ����

    void Start()
    {
        StartCoroutine(EraseWall(time));
    }

    IEnumerator EraseWall(float time)
    {
        yield return new WaitForSeconds(time);
        Wall.SetActive(false);
    }
}
