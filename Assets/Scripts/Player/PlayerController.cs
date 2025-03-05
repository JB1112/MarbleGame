using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 dragStartPos;  // �巡�� ���� ��ġ
    private Vector3 dragEndPos;    // �巡�� ���� ��ġ
    private Vector3 dragCurrentPos;
    public RectTransform arrowIndicator; // ȭ��ǥ ���� ǥ�ÿ�
    public float rotationOffset = 90f; // ȭ��ǥ ����
    public float scaleFactor = 0.05f; // ȭ��ǥ ���� ���� ����

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

    //Todo ESC �޴���ư ����, �޴� �������� �巡�� �̺�Ʈ �߻� X

    private void ShootBall()
    {
        Vector2 dragVector = dragStartPos - dragEndPos;
        Vector2 dragDirection = dragVector.normalized;

        Vector3 worldDragDirection = new Vector3(dragDirection.x, 0, dragDirection.y);

        Vector3 forward = transform.forward; // ���� �ٶ󺸴� ����
        float maxAngle = 90f;
        float angle = Vector3.Angle(forward, worldDragDirection);

        if (angle > maxAngle)
        {
            worldDragDirection = Vector3.Slerp(forward, worldDragDirection.normalized, maxAngle / angle);
            worldDragDirection *= dragVector.magnitude;
        }

        float dragDistance = dragVector.magnitude; // ���콺 �巡�� ����
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

        // ȸ�� ���� (Y�� ȸ�� ����)
        float angle = Mathf.Atan2(worldDragDirection.z, worldDragDirection.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(angle + rotationOffset, -90f, 90f);

        arrowIndicator.localRotation = Quaternion.Euler(0, 0, clampedAngle);

        // �巡�� �Ÿ� ��� & ȭ��ǥ ���� ����
        float dragDistance = dragVector.magnitude; // ���콺 �巡�� �Ÿ�
        float newHeight = Mathf.Max(dragDistance * scaleFactor, 1f);
        arrowIndicator.sizeDelta = new Vector2(arrowIndicator.sizeDelta.x, newHeight);
    }

    private void OnDisable()
    {
        GameManager.Instance.turnStart -= CheckMyTurn;
    }
}
