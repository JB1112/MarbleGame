using UnityEngine;
using UnityEngine.UI;


public class OptionUI : PopupUI
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfmSlider;
    [SerializeField] private Slider bgmSlider;

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(AudioMixerManager.Instance.SetmasterVolume);
        bgmSlider.onValueChanged.AddListener(AudioMixerManager.Instance.SetbgmVolume);
        sfmSlider.onValueChanged.AddListener(AudioMixerManager.Instance.SetsfxVolume);
    }

    public void ApplyBtn()
    {
        //Todo 슬라이드바에 따른 사운드 적용, 그냥 닫으면 원 상태로 복구
        Exit();
    }

    public void ExitBtn()
    {
        Exit();
    }

    public void Exit()
    {
        UIController.Instance.HideUI<OptionUI>();
        UIController.Instance.ShowUI<MainMenuUI>(UIs.Popup);
    }

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(AudioMixerManager.Instance.SetmasterVolume);
        bgmSlider.onValueChanged.RemoveListener(AudioMixerManager.Instance.SetbgmVolume);
        sfmSlider.onValueChanged.RemoveListener(AudioMixerManager.Instance.SetsfxVolume);
    }
}
