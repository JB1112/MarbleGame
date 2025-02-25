using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    void Start()
    {
        UIController.Instance.ShowUI<StartUI>(UIs.Scene);
        UIController.Instance.ShowUI<MainMenuUI>(UIs.Popup);
    }

}
