using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OptionUI : PopupUI
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

    public void Exit()
    {
        UIController.Instance.HideUI<OptionUI>();
        UIController.Instance.ShowUI<MainMenuUI>(UIs.Popup);
    }
}
