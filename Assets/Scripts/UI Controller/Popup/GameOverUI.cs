using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : PopupUI
{
    public List<TextMeshProUGUI> playerName;
    public List<TextMeshProUGUI> gainBalls;
    public List<TextMeshProUGUI> scores;
    public List<GameObject> WinText;
    public GameObject DrawTxt;

    private void Start()
    {
        CaculateTotalScore();
    }
    public void CaculateTotalScore()
    {
        for(int i = 0; i < 3; i++)
        {
            playerName[i].text = $"{GameManager.Instance.players[i]}";
            gainBalls[i].text = $"Beads : {GameManager.Instance.GainBalls[GameManager.Instance.mainGameTurn[i]]}";
            scores[i].text = $"Score : {GameManager.Instance.curScore[GameManager.Instance.mainGameTurn[i]]}";
        }

        int highestIndex = GetHighestIndex(scores);

        if (highestIndex == -1) // 동률이면 두 번째 리스트에서 비교
        {
            highestIndex = GetHighestIndex(gainBalls);

            if(highestIndex == -1)
            {
                DrawTxt.SetActive(true);

                return;
            }
        }

        WinText[highestIndex].SetActive(true);
    }

    public void OnRetryBtnClick()
    {
        ResourcesManager.Instance.ClearDic();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void OnTitleBtnClick()
    {
        ResourcesManager.Instance.ClearDic();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private int GetHighestIndex(List<TextMeshProUGUI> scores)
    {
        if (scores.Count == 0) return -1;

        Regex regex = new Regex(@"\d+(\.\d+)?");

        List<float> scoreValues = scores
            .Select(text => {
                Match match = regex.Match(text.text); // 문자열에서 숫자 찾기
                return match.Success ? float.Parse(match.Value) : float.MinValue;
            })
            .ToList();

        float maxValue = scoreValues.Max();

        List<int> maxIndexes = scoreValues
            .Select((value, index) => new { value, index })
            .Where(x => x.value == maxValue)
            .Select(x => x.index)
            .ToList();

        return maxIndexes.Count == 1 ? maxIndexes[0] : -1; // 동률이면 -1 반환
    }
}
