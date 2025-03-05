using UnityEngine;

public class NoticeUI : PopupUI
{

    public void OnEnable()
    {
        GameManager.Instance.isWaiting = true;
    }
    public void ResetPanel()
    {
        if(GameManager.Instance.isSetTurn)
        {
            GameManager.Instance.isWaiting = false;
        }
        GameManager.Instance.turnStart?.Invoke();
        this.gameObject.SetActive(false);
    }
}
