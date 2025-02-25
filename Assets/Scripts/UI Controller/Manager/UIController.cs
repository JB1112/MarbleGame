using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    public int curSortingOrder = 10;

    public void ShowUI<T>(UIs type)
    {
        string uiName = typeof(T).Name;

        if (ResourcesManager.Instance.CheckUIDictionary(uiName))
        {
            BaseUI ui = ResourcesManager.Instance.GetUIInDic(uiName);

            ui.gameObject.SetActive(true);

            ui.SetOrder(curSortingOrder++);
            return;
        }
        BaseUI obj = Instantiate(Resources.Load<BaseUI>($"Prefabs/{type.ToString()}/{uiName}"));
        obj.SetOrder(curSortingOrder++);

        ResourcesManager.Instance.AddUIInDic(uiName, obj);
    }

    public void HideUI<T>()
    {
        string uiName = typeof(T).Name;
        BaseUI ui = ResourcesManager.Instance.GetUIInDic(uiName);
        ui.gameObject.SetActive(false);
        curSortingOrder--;
    }
}