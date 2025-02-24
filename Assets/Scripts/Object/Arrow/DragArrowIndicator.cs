using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragArrowIndicator : MonoBehaviour
{
    public Transform ball; // ��
    public RectTransform arrowImage; // ȭ��ǥ UI
    public UnityEngine.Camera mainCamera; // ī�޶�
    public float scaleFactor = 5000f; // ȭ��ǥ ���� ���� ����
    public float rotationOffset = 90f; // ȭ��ǥ ����

    private Vector3 dragStartPos;
    private bool isDragging = false;

    void Start()
    {
        if (mainCamera == null) mainCamera = UnityEngine.Camera.main;
        arrowImage.gameObject.SetActive(false); // ������ �� ����
    }

    //void Update()
    //{
    //    if (isDragging)
    //    {
    //        Vector3 dragCurrentPos = Input.mousePosition;
    //        UpdateArrow(dragStartPos, dragCurrentPos);
    //    }

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        dragStartPos = Input.mousePosition;
    //        isDragging = true;
    //        arrowImage.gameObject.SetActive(true);
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        isDragging = false;
    //        arrowImage.gameObject.SetActive(false);
    //    }
    //}

    void UpdateArrow(Vector3 startMousePos, Vector3 endMousePos)
    {
        // ȭ�� ��ǥ �� ���� ��ǥ ��ȯ (Y�� ����)
        Vector2 dragVector = endMousePos - startMousePos;
        Vector2 dragDirection = dragVector.normalized;

        Vector3 worldDragDirection = new Vector3(dragDirection.x, 0, dragDirection.y);

        // ȸ�� ���� (Y�� ȸ�� ����)
        float angle = Mathf.Atan2(worldDragDirection.z, worldDragDirection.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(angle + rotationOffset, -90f, 90f);

        arrowImage.rotation = Quaternion.Euler(0, 0, clampedAngle);

        // �巡�� �Ÿ� ��� & ȭ��ǥ ���� ����
        float dragDistance = dragVector.magnitude; // ���콺 �巡�� �Ÿ�
        float newHeight = Mathf.Max(dragDistance * scaleFactor, 100f);
        arrowImage.sizeDelta = new Vector2(arrowImage.sizeDelta.x, newHeight);
    }
}
