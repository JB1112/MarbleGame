using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Time")]
    public float time = 10.0f;

    public GameObject SubCamera;

    public static Action decideTurn;
    public static Action GameStart;
    public static Action<Vector3> chekDistance; // 공과 엔드라인까지의 거리를 계산하기 위함
    public static Action<float> CheckScore; //순서 정하기 및 점수 계산을 위한 로직
    public static Action turnStart;
    public static Action CheckBead;
    public static Action<int> LoseBead;

    public static bool isSetTurn; //턴을 정하는 중이라면
    public static bool isIn = false;
    public static bool isWaiting = true;
    public static bool isMoving = false;

    public static int turn = 1;
    public static int PlayerNumber = 1;
    public static int outBall = 0;
    public int totalBalls = 21;
    public int feildBalls = 12;

    public static List<Transform> spwanPosition = new List<Transform>();

    public static List<string> players = new List<string>();
    public static List<int> mainGameTurn = new List<int>();

    public static List<int> haveBalls = new List<int>();
    public static List<int> GainBalls = new List<int>();
    public static List<int> curScore = new List<int>();

    public ObjectPool pools;

    private void Awake()
    {
        GameStart += DropBalls;
    }

    void Start()
    {
        turn = 1;
        isSetTurn = true;
        decideTurn?.Invoke();

        for (int i = 0; i < 3; i++)
        {
            players.Add($"Player{i + 1}");
            haveBalls.Add(3);
            GainBalls.Add(0);
            curScore.Add(0);
        }

    }

    private void DropBalls()
    {
        SubCamera.SetActive(true);

        for (int i = 0; i < feildBalls; i++)
        {
            pools.SpawnFromPool("Bead", spwanPosition[i].position);
        }

        CheckBead += UpdateBallCount;
        LoseBead += LoseBall;
    }

    private void UpdateBallCount()
    {
        int i = turn - 2;
        if (i < 0)
        {
            i = 2;
        }


        if (outBall < 0 || isIn)
        {
            int dropAmount = GainBalls[i] + Math.Abs(outBall) + 1;
            LoseBead?.Invoke(dropAmount);
            Debug.Log(dropAmount);
            haveBalls[i] = haveBalls[i] - 1;
            feildBalls = feildBalls + GainBalls[i] + 1;
            GainBalls[i] = 0;
            Debug.Log($"필드 공{feildBalls}");
            isIn = false;
            outBall = 0;

            return;
        }

        GainBalls[i] = GainBalls[i] + outBall;
        feildBalls = feildBalls - outBall;
        outBall = 0;

        CheckGameOver();
    }

    public void LoseBall(int num)
    {
        for(int i = 0; i< num; i++)
        {
            int j = UnityEngine.Random.Range(0, 21);
            pools.SpawnFromPool("Bead", spwanPosition[j].position);
        }
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
                turnStart?.Invoke();

                return;
            }
        }
        CheckBead?.Invoke();
        turnStart?.Invoke();
    }

    private void OnDisable()
    {
        GameStart -= DropBalls;

        if(!isSetTurn)
        {
            CheckBead -= UpdateBallCount;
            LoseBead -= LoseBall;
        }
    }

    private void CheckGameOver()
    {
        if (feildBalls == 0)
        {
            Time.timeScale = 0;

            UIController.Instance.ShowUI<GameOverUI>(UIs.Popup);


        }
    }
}
