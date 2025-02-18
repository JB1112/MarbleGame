using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragArrowIndicator : MonoBehaviour
{
    public Transform ball; // 공
    public RectTransform arrowImage; // 화살표 UI
    public UnityEngine.Camera mainCamera; // 카메라
    public float arrowOffset = 5.0f; // 공 앞에 위치할 거리
    public float scaleFactor = 5000f; // 화살표 길이 증가 비율
    public float rotationOffset = 180f; // 화살표 보정

    private Vector3 dragStartPos;
    private bool isDragging = false;

    void Start()
    {
        if (mainCamera == null) mainCamera = UnityEngine.Camera.main;
        arrowImage.gameObject.SetActive(false); // 시작할 때 숨김
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
        // 화면 좌표 → 월드 좌표 변환 (Y축 고정)
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.transform.position.y));
        worldMousePos.y = ball.position.y; // Y축 고정

        // 방향 벡터 계산
        Vector3 direction = (worldMousePos - ball.position).normalized;

        // 회전 설정 (Y축 회전 없이)
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; // 방향이 올바르게 적용되지 않는 상황
        arrowImage.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        // 드래그 거리 계산 & 화살표 길이 조정
        float dragDistance = Vector3.Distance(worldMousePos, ball.position);
        float newHeight = Mathf.Max(dragDistance * scaleFactor, 100f); //드래그 크기에 따른 크기 증가가 잘 적용되지 않음.
        arrowImage.sizeDelta = new Vector2(arrowImage.sizeDelta.x, newHeight);
    }
}
