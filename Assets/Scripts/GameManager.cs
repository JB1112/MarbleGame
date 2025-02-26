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
    public static Action<Vector3> chekDistance; // 공과 엔드라인까지의 거리를 계산하기 위함
    public static Action<float> CheckScore; //순서 정하기 및 점수 계산을 위한 로직
    public static Action turnStart;

    public static bool isSetTurn; //턴을 정하는 중이라면
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
        //임시로 오브젝트 활성화, 나중에 SpwanPosition을 두어 하늘에서 떨어지도록 설정
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
