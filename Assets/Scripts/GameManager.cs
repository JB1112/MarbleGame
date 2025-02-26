using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Time")]
    public float time = 10.0f;

    public static Action decideTurn;
    public static Action GameStart;
    public static Action<Vector3> chekDistance; // ���� ������α����� �Ÿ��� ����ϱ� ����
    public static Action<float> CheckScore; //���� ���ϱ� �� ���� ����� ���� ����
    public static Action turnStart;

    public static bool isSetTurn; //���� ���ϴ� ���̶��
    public static bool isIn = false;
    public static bool isWaiting = true;
    public static bool isMoving = false;

    public static int turn = 1;
    public static int PlayerNumber = 1;

    public List<GameObject> balls = new List<GameObject>();

    void Start()
    {
        GameStart += DropBalls;

        isSetTurn = true;
        decideTurn?.Invoke();
    }

    private void DropBalls()
    {
        //�ӽ÷� ������Ʈ Ȱ��ȭ, ���߿� SpwanPosition�� �ξ� �ϴÿ��� ���������� ����
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].SetActive(true);
        }

        turnStart?.Invoke();
    }

    void ResetGame()
    {
        ResourcesManager.Instance.ClearDic();
        SceneManager.LoadScene(1);
    }

    void LoadStartScene()
    {
        ResourcesManager.Instance.ClearDic();
        SceneManager.LoadScene(0);
    }

    public static void turnChange()
    {
        turn++;

        if(turn >3)
        {
            turn = 1;

            if (isSetTurn)
            {
                isSetTurn = false;
                GameStart?.Invoke();

                return;
            }
        }

        turnStart?.Invoke();
    }
}
