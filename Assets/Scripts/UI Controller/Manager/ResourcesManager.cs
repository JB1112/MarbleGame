using System.Collections.Generic;
using UnityEngine;

public enum UIs
{
    Popup,
    Scene,
}

public class ResourcesManager : Singleton<ResourcesManager>
{
    private Dictionary<string, BaseUI> uiDictionary = new Dictionary<string, BaseUI>();

    public GameObject GetUI(UIs type, string uiName)
    {
        return Resources.Load<GameObject>($"Prefabs/{type.ToString()}/{uiName}");
    }

    public BaseUI GetUIInDic(string uiName)
    {
        return uiDictionary[uiName];
    }

    public bool CheckUIDictionary(string uiName)
    {
        return uiDictionary.ContainsKey(uiName);
    }

    public void AddUIInDic(string uiName, BaseUI obj)
    {
        uiDictionary.Add(uiName, obj);
    }

    public void ClearDic()
    {
        uiDictionary.Clear();
        UIController.Instance.curSortingOrder = 10;
    }
}
