using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 dragStartPos;  // 드래그 시작 위치
    private Vector3 dragEndPos;    // 드래그 종료 위치
    private Vector3 dragCurrentPos;
    public RectTransform arrowIndicator; // 화살표 방향 표시용
    public float rotationOffset = 90f; // 화살표 보정
    public float scaleFactor = 0.05f; // 화살표 길이 증가 비율

    public AudioSource audio;

    private float dragStartTime; 
    private const float minDragTime = 0.2f; 


    private bool isDragging = false;
    private bool isMyTurn = true;

    public float forceMultiplier;
    private Rigidbody rb;
    public UnityEngine.Camera mainCamera;

    public PlayerInput playerInput;
    private BallsMovement move;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        move = GetComponent<BallsMovement>();
    }

    private void Start()
    {
        GameManager.Instance.turnStart += CheckMyTurn;
    }

    private void Update()
    {
        if (isDragging)
        {
            dragCurrentPos = Input.mousePosition;
            UpdateArrow(dragStartPos, dragCurrentPos);
        }
    }

    private void CheckMyTurn()
    {
        if (GameManager.Instance.mainGameTurn[GameManager.Instance.turn-1] <= GameManager.Instance.PlayerNumber-1)
        {
            GameManager.Instance.isMoving = false;
            isMyTurn = true;
        }
    }

    public void OnDragInput(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.isMoving || GameManager.Instance.isWaiting || !isMyTurn || Time.timeScale!=1)
        {
            return;
        }

        if (context.phase == InputActionPhase.Started)
        {
            dragStartTime = Time.time;
            dragStartPos = Input.mousePosition;
            isDragging = true;
            arrowIndicator.gameObject.SetActive(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            float dragDuration = Time.time - dragStartTime;

            if(dragDuration < minDragTime)
            {
                isDragging = false;
                arrowIndicator.gameObject.SetActive(false);
                return;
            }

            dragEndPos = Input.mousePosition;
            isDragging = false;
            ShootBall();
            GameManager.Instance.isMoving = true;
            arrowIndicator.gameObject.SetActive(false);
        }
    }

    //Todo ESC 메뉴버튼 생성, 메뉴 눌렀을시 드래그 이벤트 발생 X

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

        isMyTurn = false;

        audio.Play();

        move.CheckStop();
    }

    void UpdateArrow(Vector3 startMousePos, Vector3 endMousePos)
    {
        Vector2 dragVector = endMousePos - startMousePos;
        Vector2 dragDirection = dragVector.normalized;

        Vector3 worldDragDirection = new Vector3(dragDirection.x, 0, dragDirection.y);

        // 회전 설정 (Y축 회전 없이)
        float angle = Mathf.Atan2(worldDragDirection.z, worldDragDirection.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(angle + rotationOffset, -90f, 90f);

        arrowIndicator.localRotation = Quaternion.Euler(0, 0, clampedAngle);

        // 드래그 거리 계산 & 화살표 길이 조정
        float dragDistance = dragVector.magnitude; // 마우스 드래그 거리
        float newHeight = Mathf.Max(dragDistance * scaleFactor, 1f);
        arrowIndicator.sizeDelta = new Vector2(arrowIndicator.sizeDelta.x, newHeight);
    }

    private void OnDisable()
    {
        GameManager.Instance.turnStart -= CheckMyTurn;
    }
}
