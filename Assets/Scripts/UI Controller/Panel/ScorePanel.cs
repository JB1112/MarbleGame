using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{
    public List<GameObject> turnchecks;
    public List<TextMeshProUGUI> scores;

    public float curScore = 00;

    private void Awake()
    {
        GameManager.turnStart += ChangePlayer;
        GameManager.CheckScore += WriteScore;
    }

    private void WriteScore(float obj)
    {
        int i = GameManager.turn - 1;

        if(obj < 0)
        {
            scores[i].text = curScore.ToString(); //Todo 나중에 점수 차감하는 로직 작성
        }
        else
        {
            float plus = obj * 10;

            scores[i].text = (float.Parse(scores[i].text) + plus).ToString();
        }

        GameManager.curScore[i] = int.Parse(scores[i].text);
    }

    private void ChangePlayer()
    {
        int pre = GameManager.preTurn -1;
        int post = GameManager.turn - 1;

        turnchecks[pre].SetActive(false);
        turnchecks[post].SetActive(true);

        curScore = float.Parse(scores[post].text);
    }

    private void OnDisable()
    {
        GameManager.turnStart -= ChangePlayer;
        GameManager.CheckScore -= WriteScore;
    }
}
