using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainSceneUI : SceneUI
{
    public GameObject TurnPanel;
    public GameObject ScorePanel;
    public GameObject BeadsPanel;

    public List<TextMeshProUGUI> Distances;
    public TextMeshProUGUI ScorePanelName;


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
                CompareValue.Add((value, i+1));
            }
        }

        CompareValue = CompareValue.OrderBy(x => x.value).ToList();

        ScorePanelName.text = $"Player{CompareValue[0].index.ToString()}\n\nPlayer{CompareValue[1].index.ToString()}\n\nPlayer{CompareValue[2].index.ToString()}";


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

    private void OnDisable()
    {
        GameManager.decideTurn -= ShowNoticePanel;
        GameManager.GameStart -= CheckTurn;
    }
}
