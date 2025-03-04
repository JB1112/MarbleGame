using UnityEngine;

public class NoticeUI : PopupUI
{

    public void OnEnable()
    {
        GameManager.isWaiting = true;
    }
    public void ResetPanel()
    {
        if(GameManager.isSetTurn)
        {
            GameManager.isWaiting = false;
        }
        GameManager.turnStart?.Invoke();
        this.gameObject.SetActive(false);
    }
}
