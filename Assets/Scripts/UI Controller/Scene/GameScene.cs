using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    private void Awake()
    {
        UIController.Instance.ShowUI<MainSceneUI>(UIs.Scene);
    }
}
