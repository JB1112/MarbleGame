using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideUI : PopupUI
{
    //Todo - �� ������ �ѱ�� ��ư, ĭ�� ������ �̹���, �ؽ�Ʈ ��ȯ,

    public void CloseGuide()
    {
        UIController.Instance.HideUI<GuideUI>();
        UIController.Instance.ShowUI<MainMenuUI>(UIs.Popup);
    }
}
