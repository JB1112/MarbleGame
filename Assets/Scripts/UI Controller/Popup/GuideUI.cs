using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideUI : PopupUI
{
    //Todo - 양 옆으로 넘기는 버튼, 칸에 삽입할 이미지, 텍스트 변환,

    public void CloseGuide()
    {
        UIController.Instance.HideUI<GuideUI>();
        UIController.Instance.ShowUI<MainMenuUI>(UIs.Popup);
    }
}
