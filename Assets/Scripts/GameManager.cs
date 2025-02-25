using System;
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
