using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneUI : SceneUI
{
    public GameObject TurnPanel;
    public GameObject ScorePanel;
    public GameObject BeadsPanel;

    public List<TextMeshProUGUI> Distances;
    public List<TextMeshProUGUI> ScorePanelName;
    public List<TextMeshProUGUI> BeadsPanelName;


    private void Awake()
    {
        GameManager.decideTurn += ShowNoticePanel;
        GameManager.GameStart += CheckTurn;
    }

    private void CheckTurn()
    {
        List<(float value, int index)> CompareValue = new List<(float, int)>();

        for (int i = 0; i < Distances.Count; i++)
        {
            if (float.TryParse(Distances[i].text, out float value))
            {
                CompareValue.Add((value, i));
            }
        }

        CompareValue = CompareValue.OrderBy(x => x.value).ToList();

        GameManager.mainGameTurn.Clear();

        for (int i = 0;i < CompareValue.Count;i++)
        {
            GameManager.mainGameTurn.Add(CompareValue[i].index);
            ScorePanelName[i].text = GameManager.players[GameManager.mainGameTurn[i]];
            BeadsPanelName[i].text = GameManager.players[GameManager.mainGameTurn[i]];
        }

        UISetting();
        ShowNoticePanel();
    }

    private void UISetting()
    {
        TurnPanel.SetActive(false);
        ScorePanel.SetActive(true);
        BeadsPanel.SetActive(true);
    }

    private void ShowNoticePanel()
    {
        if(GameManager.isSetTurn == true)
        {
            UIController.Instance.ShowUI<NoticeUI>(UIs.Popup);

        }
        else
        {
            UIController.Instance.ShowUI<NoticeUI2>(UIs.Popup);
        }
    }

    public void OnOptionBtnClick()
    {
        UIController.Instance.ShowUI<MainOptionUI>(UIs.Popup);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        GameManager.decideTurn -= ShowNoticePanel;
        GameManager.GameStart -= CheckTurn;
    }
}
