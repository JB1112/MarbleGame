using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Time")]
    public float time = 10.0f;

    public static Action decideTurn;
    public static Action turnStart;
    public bool isIn;
    public int turn;

    void Start()
    {
        decideTurn();
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
}
