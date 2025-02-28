using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeUI2 : PopupUI
{
    public void OnEnable()
    {
        GameManager.isWaiting = true;
    }
    public void ResetPanel()
    {
        this.gameObject.SetActive(false);
        GameManager.isWaiting = false;
    }
}
