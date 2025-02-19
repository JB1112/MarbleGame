using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsMovement : MonoBehaviour
{
    private Vector3 dragStartPos;  // 드래그 시작 위치
    private Vector3 dragEndPos;    // 드래그 종료 위치
    private bool isDragging = false;

    public float forceMultiplier;
    private Rigidbody rb;
    public UnityEngine.Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = UnityEngine.Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            dragEndPos = Input.mousePosition;
            ShootBall();
            isDragging = false;
        }
    }

    // 공을 발사하는 함수
    private void ShootBall()
    {
        Vector2 dragVector = dragStartPos - dragEndPos;
        Vector2 dragDirection = dragVector.normalized;

        Vector3 worldDragDirection = new Vector3(dragDirection.x, 0, dragDirection.y);

        Vector3 forward = transform.forward; // 공이 바라보는 방향
        float maxAngle = 90f;
        float angle = Vector3.Angle(forward, worldDragDirection);

        if (angle > maxAngle)
        {
            worldDragDirection = Vector3.Slerp(forward, worldDragDirection.normalized, maxAngle / angle);
            worldDragDirection *= dragVector.magnitude;
        }

        float dragDistance = dragVector.magnitude; // 마우스 드래그 길이
        Vector3 force = worldDragDirection.normalized * dragDistance * forceMultiplier;

        rb.AddForce(force, ForceMode.Impulse);
    }
}
