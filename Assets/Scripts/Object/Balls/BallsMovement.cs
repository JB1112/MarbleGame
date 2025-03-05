using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsMovement : MonoBehaviour
{
    private Vector3 returnPoint;  // ��ȯ�� ��ġ
    public float stopThreshold = 0.1f;
    public float checkDelay = 1f;

    public AudioSource audio;

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
        if(GameManager.Instance.isSetTurn == true)
        {
            GameManager.Instance.chekDistance?.Invoke(transform.position);
        }
        else if(transform.position.y < 0 || GameManager.Instance.isIn)
        {
            GameManager.Instance.outBall = -GameManager.Instance.outBall;
            GameManager.Instance.CheckScore(GameManager.Instance.outBall);
        }
        audio.Stop();

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = returnPoint;
        transform.rotation = Quaternion.identity;
        isReturning = false;  // �ٽ� ������ �� �ֵ��� �ʱ�ȭ
        GameManager.Instance.turnChange();
    }
}
