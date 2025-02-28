using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : PopupUI
{
    public List<TextMeshProUGUI> gainBalls;
    public List<TextMeshProUGUI> scores;
    public List<GameObject> WinText;

    private void Start()
    {
        CaculateTotalScore();
    }
    public void CaculateTotalScore()
    {
        for(int i = 0; i < 3; i++)
        {
            gainBalls[i].text = $"Beads : {GameManager.GainBalls[i]}";
            scores[i].text = $"Score : {GameManager.curScore[i]}";
        }

        int highestIndex = GetHighestIndex(GameManager.curScore);

        if (highestIndex == -1) // 동률이면 두 번째 리스트에서 비교
        {
            highestIndex = GetHighestIndex(GameManager.GainBalls);
        }

        WinText[highestIndex].SetActive(true);
    }

    public void OnRetryBtnClick()
    {
        GameManager.haveBalls.Clear();
        GameManager.GainBalls.Clear();
        ResourcesManager.Instance.ClearDic();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void OnTitleBtnClick()
    {
        GameManager.haveBalls.Clear();
        GameManager.GainBalls.Clear();
        ResourcesManager.Instance.ClearDic();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private int GetHighestIndex(List<int> scores)
    {
        if (scores.Count == 0) return -1;

        float maxValue = scores.Max();
        List<int> maxIndexes = scores
            .Select((value, index) => new { value, index })
            .Where(x => x.value == maxValue)
            .Select(x => x.index)
            .ToList();

        return maxIndexes.Count == 1 ? maxIndexes[0] : -1; // 동률이면 -1 반환
    }
}
