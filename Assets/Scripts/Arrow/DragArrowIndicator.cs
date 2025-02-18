using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragArrowIndicator : MonoBehaviour
{
    public Transform ball; // ��
    public RectTransform arrowImage; // ȭ��ǥ UI
    public UnityEngine.Camera mainCamera; // ī�޶�
    public float arrowOffset = 5.0f; // �� �տ� ��ġ�� �Ÿ�
    public float scaleFactor = 5000f; // ȭ��ǥ ���� ���� ����
    public float rotationOffset = 180f; // ȭ��ǥ ����

    private Vector3 dragStartPos;
    private bool isDragging = false;

    void Start()
    {
        if (mainCamera == null) mainCamera = UnityEngine.Camera.main;
        arrowImage.gameObject.SetActive(false); // ������ �� ����
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 dragCurrentPos = Input.mousePosition;
            UpdateArrow(dragCurrentPos);
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Input.mousePosition;
            isDragging = true;
            arrowImage.gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            arrowImage.gameObject.SetActive(false);
        }
    }

    void UpdateArrow(Vector3 mousePos)
    {
        // ȭ�� ��ǥ �� ���� ��ǥ ��ȯ (Y�� ����)
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.transform.position.y));
        worldMousePos.y = ball.position.y; // Y�� ����

        // ���� ���� ���
        Vector3 direction = (worldMousePos - ball.position).normalized;

        // ȸ�� ���� (Y�� ȸ�� ����)
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; // ������ �ùٸ��� ������� �ʴ� ��Ȳ
        arrowImage.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        // �巡�� �Ÿ� ��� & ȭ��ǥ ���� ����
        float dragDistance = Vector3.Distance(worldMousePos, ball.position);
        float newHeight = Mathf.Max(dragDistance * scaleFactor, 100f); //�巡�� ũ�⿡ ���� ũ�� ������ �� ������� ����.
        arrowImage.sizeDelta = new Vector2(arrowImage.sizeDelta.x, newHeight);
    }
}
