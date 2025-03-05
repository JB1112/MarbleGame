using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerManager : Singleton<AudioMixerManager>
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetbgmVolume(float volume)
    {
        float adjustedVolume = Mathf.Clamp(volume, 0.0001f, 1f);

        audioMixer.SetFloat("BGM", Mathf.Log10(adjustedVolume) * 20);
        GameManager.Instance.BGM = volume;
    }

    public void SetsfxVolume(float volume)
    {
        float adjustedVolume = Mathf.Clamp(volume, 0.0001f, 1f);

        audioMixer.SetFloat("SFX", Mathf.Log10(adjustedVolume) * 20);
        GameManager.Instance.EffectSoundVolume = volume;
    }

    public void SetmasterVolume(float volume)
    {
        float adjustedVolume = Mathf.Clamp(volume, 0.0001f, 1f);

        audioMixer.SetFloat("Master", Mathf.Log10(adjustedVolume) * 20);
        GameManager.Instance.masterVolume = volume;
    }
}
