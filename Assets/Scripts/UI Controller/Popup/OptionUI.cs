using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OptionUI : PopupUI
{
    public void ApplyBtn()
    {
        //Todo 슬라이드바에 따른 사운드 적용, 그냥 닫으면 원 상태로 복구
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
