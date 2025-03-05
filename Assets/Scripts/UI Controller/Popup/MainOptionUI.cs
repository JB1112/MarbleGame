using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainOptionUI : PopupUI
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfmSlider;
    [SerializeField] private Slider bgmSlider;

    private void OnEnable()
    {
        masterSlider.value = GameManager.Instance.masterVolume;
        sfmSlider.value = GameManager.Instance.EffectSoundVolume;
        bgmSlider.value = GameManager.Instance.BGM;

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

    public void OnMainBtnClick()
    {
        ResourcesManager.Instance.ClearDic();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        UIController.Instance.HideUI<MainOptionUI>();
    }

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(AudioMixerManager.Instance.SetmasterVolume);
        bgmSlider.onValueChanged.RemoveListener(AudioMixerManager.Instance.SetbgmVolume);
        sfmSlider.onValueChanged.RemoveListener(AudioMixerManager.Instance.SetsfxVolume);
    }
}
