using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainOptionUI : PopupUI
{
    public void ApplyBtn()
    {
        //Todo �����̵�ٿ� ���� ���� ����, �׳� ������ �� ���·� ����
        Exit();
    }

    public void ExitBtn()
    {
        Exit();
    }

    public void OnMainBtnClick()
    {
        ResourcesManager.Instance.ClearDic();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        UIController.Instance.HideUI<MainOptionUI>();
    }
}
