using System;
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
    public static bool isIn;
    public static int turn = 1;

    void Start()
    {
        isSetTurn = true;
        Debug.Log(isSetTurn);
        decideTurn?.Invoke();

        turnStart += turnChange;
    }

    void ResetGame()
    {
        SceneManager.LoadScene(1);
    }

    void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    void turnChange()
    {
        turn++;

        if(turn >3)
        {
            turn = 1;
        }
    }

    private void OnDisable()
    {
        turnStart -= turnChange;
    }
}
