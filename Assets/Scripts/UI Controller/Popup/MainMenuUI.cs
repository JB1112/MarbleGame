using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : PopupUI
{
    public void GameStart()
    {
        ResourcesManager.Instance.ClearDic();
        SceneManager.LoadScene(1);
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
            //Application.Quit();
#endif
    }

    public void CloseMenu()
    {
        UIController.Instance.HideUI<MainMenuUI>();
    }
}
