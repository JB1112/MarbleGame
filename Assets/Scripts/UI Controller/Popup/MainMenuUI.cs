using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : PopupUI
{
    public void GameStart()
    {
        UIController.Instance.ShowUI<ChoicePlayerUI>(UIs.Popup);
    }

    public void OpenOption()
    {
        UIController.Instance.ShowUI<OptionUI>(UIs.Popup);
        CloseMenu();
    }

    public void OpenGuide()
    {
        UIController.Instance.ShowUI<GuideUI>(UIs.Popup);
        CloseMenu();
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    #endif
    }

    public void CloseMenu()
    {
        UIController.Instance.HideUI<MainMenuUI>();
    }
}
