using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsMovement : MonoBehaviour
{
    private Vector3 dragStartPos;  // �巡�� ���� ��ġ
    private Vector3 dragEndPos;    // �巡�� ���� ��ġ
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
            return hit.point; // �浹�� ���� ��ȯ
        }
        return Vector3.zero;
    }

    // ���� �߻��ϴ� �Լ�
    private void ShootBall()
    {
        Vector3 direction = dragStartPos - dragEndPos;
        direction.y = 0; // Y�� ���� (���� ���⸸ ����)

        Vector3 forward = transform.forward; // ���� �ٶ󺸴� ���� ����

        float maxAngle = 90f;
        float angle = Vector3.Angle(forward, direction);

        if (angle > maxAngle)
        {
            // �ִ� ��� ������ ���� (���� �������� ����� �������� ����)
            direction = Vector3.Slerp(forward, direction.normalized, maxAngle / angle);
            direction *= (dragStartPos - dragEndPos).magnitude;
        }

        float dragDistance = direction.magnitude;
        Vector3 force = direction.normalized * dragDistance * forceMultiplier;

        rb.AddForce(force, ForceMode.Impulse);
    }
}
