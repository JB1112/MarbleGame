using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsMovement : MonoBehaviour
{
    private Vector3 returnPoint;  // ��ȯ�� ��ġ
    public float stopThreshold = 0.05f;
    public float checkDelay = 1f;

    private Rigidbody rb;
    private bool isReturning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        returnPoint = transform.position;
    }

    public void CheckStop()
    {
        StartCoroutine(CheckIfStopped());
    }

    IEnumerator CheckIfStopped()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkDelay);  // ���� �ð� �������� üũ

            if (!isReturning && rb.velocity.magnitude < stopThreshold || transform.position.y < 0)
            {
                isReturning = true;  // �ߺ� ���� ����
                ReturnBall();
                yield break;
            }
        }
    }

    void ReturnBall()
    {
        if(GameManager.isSetTurn == true)
        {
            GameManager.chekDistance?.Invoke(transform.position);
        }
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = returnPoint;
        transform.rotation = Quaternion.identity;
        isReturning = false;  // �ٽ� ������ �� �ֵ��� �ʱ�ȭ
        GameManager.turnChange();
    }
}
