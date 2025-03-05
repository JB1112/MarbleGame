using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoicePlayerUI : PopupUI
{
    public TextMeshProUGUI Number;
    int i = 1;

    public void OnLeftBtnClick()
    {
        if(i == 1)
        {
            return;
        }

        i--;

        Number.text = i.ToString();
    }

    public void OnRightBtnClick()
    {
        if(i == 3)
        {
            return;
        }

        i++;

        Number.text = i.ToString();
    }

    public void OnApplyClick()
    {
        ResourcesManager.Instance.ClearDic();
        GameManager.Instance.PlayerNumber = i;
        SceneManager.LoadScene(1);
    }

    public void OnCanCleClick()
    {
        UIController.Instance.HideUI<ChoicePlayerUI>();
    }
}
