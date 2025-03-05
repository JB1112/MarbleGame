using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Time")]
    public float time = 10.0f;

    public GameObject SubCamera;

    public Action decideTurn;
    public Action GameStart;
    public Action<Vector3> chekDistance; // 공과 엔드라인까지의 거리를 계산하기 위함
    public Action<float> CheckScore; //순서 정하기 및 점수 계산을 위한 로직
    public Action turnStart;
    public Action CheckBead;
    public Action<int> LoseBead;

    public bool isSetTurn; //턴을 정하는 중이라면
    public bool isIn = false;
    public bool isWaiting = true;
    public bool isMoving = false;

    private bool isSceneLoaded = false;

    public float masterVolume = 1;
    public float BGM = 1;
    public float EffectSoundVolume = 1;

    public int turn = 1;
    public int preTurn;
    public int PlayerNumber = 1;
    public int outBall = 0;
    public int totalBalls = 21;
    public int feildBalls = 12;

    public List<Transform> spwanPosition = new List<Transform>();

    public List<string> players = new List<string>();
    public List<int> mainGameTurn = new List<int>();
    public List<bool> isOver = new List<bool>();

    public List<int> haveBalls = new List<int>();
    public List<int> GainBalls = new List<int>();
    public List<int> curScore = new List<int>();

    public ObjectPool pools;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameStart -= DropBalls;

        if (!isSetTurn)
        {
            CheckBead -= UpdateBallCount;
            LoseBead -= LoseBall;
            isSetTurn = true;
        }


        if (scene.buildIndex == 1)
        {
            isSceneLoaded = false;

            if (!isSceneLoaded)
            {
                isSceneLoaded = true;
                SceneLoad();
            }
        }
    }


    public void SceneLoad()
    {
        GameStart += DropBalls;
        turn = 1;
        isSetTurn = true;
        isIn = false;
        isWaiting = true;
        isMoving = false;
        outBall = 0;
        totalBalls = 21;
        feildBalls = 12;
        
        players.Clear();
        mainGameTurn.Clear();
        haveBalls.Clear();
        GainBalls.Clear();
        curScore.Clear();
        isOver.Clear();

        for (int i = 0; i < 3; i++)
        {
            if (i < PlayerNumber)
            {
                players.Add($"Player{i + 1}");
            }
            else
            {
                players.Add($"COM{i-(PlayerNumber-1)}");
            }
            mainGameTurn.Add(i);
            haveBalls.Add(3);
            GainBalls.Add(0);
            curScore.Add(0);
            isOver.Add(false);
        }
        decideTurn?.Invoke();
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
        int i = preTurn - 1;

        if (outBall < 0 || isIn)
        {
            int dropAmount = GainBalls[i] + Math.Abs(outBall) + 1;
            LoseBead?.Invoke(dropAmount);
            haveBalls[i] = haveBalls[i] - 1;
            feildBalls = feildBalls + GainBalls[i] + 1;
            GainBalls[i] = 0;
            isIn = false;
            outBall = 0;

            checkYouFire();

            return;
        }

        GainBalls[i] = GainBalls[i] + outBall;
        feildBalls = feildBalls - outBall;
        outBall = 0;

        CheckGameOver();
    }

    private void checkYouFire()
    {
        int i = preTurn - 1;

        if (haveBalls[i] == 0)
        {
            isOver[i] = true;
        }

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

    public void turnChange()
    {
        preTurn = turn;
        int startTurn = turn;
        bool allOver = true;

        do
        {
            turn++;

            if (turn > 3)
            {
                turn = 1;
            }

            if (!isOver[turn - 1])
            {
                allOver = false;
                break;
            }

        } while (turn != startTurn);

        if (allOver)
        {
            Time.timeScale = 0;
            UIController.Instance.ShowUI<GameOverUI>(UIs.Popup);
            return;
        }

        if (turn == 1 && isSetTurn)
        {
            isSetTurn = false;
            isIn = false;
            GameStart?.Invoke();
            turnStart?.Invoke();
            return;
        }

        CheckBead?.Invoke();
        turnStart?.Invoke();
    }

    private void CheckGameOver()
    {
        bool allOver = false;

        for(int i = 0; i < isOver.Count; i++)
        {
            if (!isOver[i])
            {
                allOver = false;
                break;
            }

            allOver = true;
        }

        if (feildBalls == 0 || allOver)
        {
            Time.timeScale = 0;

            UIController.Instance.ShowUI<GameOverUI>(UIs.Popup);


        }
    }
}
