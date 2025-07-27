using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    [SerializeField] private AudioSource bgmAudioPlayer;
    [SerializeField] private AudioSource[] sfxAudioPlayer;
    private void Start()
    {
        LoadSetting();
    }
    private void LoadSetting()
    {
        SettingData settingData = SettingHandler.instance.LoadSetting();
        if (settingData != null)
        {
            if (settingData.bgmVolume > settingData.masterVolume)
            {
                bgmAudioPlayer.volume = settingData.masterVolume;
            }
            else
            {
                bgmAudioPlayer.volume = settingData.bgmVolume;
            }
            if (settingData.sfxVolume > settingData.masterVolume)
            {
                foreach (AudioSource sfxSource in sfxAudioPlayer)
                {
                    sfxSource.volume = settingData.masterVolume;
                }
            }
            else
            {
                foreach (AudioSource sfxSource in sfxAudioPlayer)
                {
                    sfxSource.volume = settingData.sfxVolume;
                }
            }
        }
    }
    public void UpdateSetting()
    {
        LoadSetting();
    }
}
