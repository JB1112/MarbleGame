using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsMovement : MonoBehaviour
{
    private Vector3 returnPoint;  // 반환할 위치
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
            yield return new WaitForSeconds(checkDelay);  // 일정 시간 간격으로 체크

            if (!isReturning && rb.velocity.magnitude < stopThreshold || transform.position.y < 0)
            {
                isReturning = true;  // 중복 실행 방지
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
        isReturning = false;  // 다시 움직일 수 있도록 초기화
        GameManager.turnChange();
    }
}
