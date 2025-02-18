using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsMovement : MonoBehaviour
{
    private Vector3 dragStartPos;  // 드래그 시작 위치
    private Vector3 dragEndPos;    // 드래그 종료 위치
    private bool isDragging = false;

    public float forceMultiplier = 20f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = GetMouseWorldPosition();
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            dragEndPos = GetMouseWorldPosition();
            ShootBall();
            isDragging = false;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point; // 충돌한 지점 반환
        }
        return Vector3.zero;
    }

    // 공을 발사하는 함수
    private void ShootBall()
    {
        Vector3 direction = dragStartPos - dragEndPos;
        direction.y = 0; // Y축 고정 (수평 방향만 적용)

        Vector3 forward = transform.forward; // 공이 바라보는 정면 방향

        float maxAngle = 90f;
        float angle = Vector3.Angle(forward, direction);

        if (angle > maxAngle)
        {
            // 최대 허용 각도로 보정 (정면 기준으로 가까운 방향으로 조정)
            direction = Vector3.Slerp(forward, direction.normalized, maxAngle / angle);
            direction *= (dragStartPos - dragEndPos).magnitude;
        }

        float dragDistance = direction.magnitude;
        Vector3 force = direction.normalized * dragDistance * forceMultiplier;

        rb.AddForce(force, ForceMode.Impulse);
    }
}
