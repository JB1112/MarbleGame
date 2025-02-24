using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragArrowIndicator : MonoBehaviour
{
    public Transform ball; // 공
    public RectTransform arrowImage; // 화살표 UI
    public UnityEngine.Camera mainCamera; // 카메라
    public float scaleFactor = 5000f; // 화살표 길이 증가 비율
    public float rotationOffset = 90f; // 화살표 보정

    private Vector3 dragStartPos;
    private bool isDragging = false;

    void Start()
    {
        if (mainCamera == null) mainCamera = UnityEngine.Camera.main;
        arrowImage.gameObject.SetActive(false); // 시작할 때 숨김
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
        // 화면 좌표 → 월드 좌표 변환 (Y축 고정)
        Vector2 dragVector = endMousePos - startMousePos;
        Vector2 dragDirection = dragVector.normalized;

        Vector3 worldDragDirection = new Vector3(dragDirection.x, 0, dragDirection.y);

        // 회전 설정 (Y축 회전 없이)
        float angle = Mathf.Atan2(worldDragDirection.z, worldDragDirection.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(angle + rotationOffset, -90f, 90f);

        arrowImage.rotation = Quaternion.Euler(0, 0, clampedAngle);

        // 드래그 거리 계산 & 화살표 길이 조정
        float dragDistance = dragVector.magnitude; // 마우스 드래그 거리
        float newHeight = Mathf.Max(dragDistance * scaleFactor, 100f);
        arrowImage.sizeDelta = new Vector2(arrowImage.sizeDelta.x, newHeight);
    }
}
