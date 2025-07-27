using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Main_Setting : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private GameObject setting_ConfirmButton;
    [Header("Sounds")]
    [SerializeField] private AudioSource setting_BGMAudioSouce;
    [SerializeField] private AudioSource setting_SFXAudioSouce;
    [Header("Slider")]
    [SerializeField] private Slider setting_MasterSlider;
    [SerializeField] private Slider setting_BGMSlider;
    [SerializeField] private Slider setting_SFXSlider;
    [SerializeField] private TMP_Text setting_MasterValue;
    [SerializeField] private TMP_Text setting_BGMValue;
    [SerializeField] private TMP_Text setting_SFXValue;

    private float setting_OriginalMasterVolume;
    private float setting_OriginalBGMVolume;
    private float setting_OriginalSFXVolume;
    private void OnEnable()
    {
        setting_OriginalMasterVolume = setting_MasterSlider.value;
        setting_OriginalBGMVolume = setting_BGMSlider.value;
        setting_OriginalSFXVolume = setting_SFXSlider.value;
    }
    private void OnDisable()
    {
        // Reset all setting to the original value if player doesn't confirm the setting
        setting_MasterSlider.value = setting_OriginalMasterVolume;
        setting_BGMSlider.value = setting_OriginalBGMVolume;
        setting_SFXSlider.value = setting_OriginalSFXVolume;
        setting_ConfirmButton.SetActive(false);

        // Master
        MasterSetting();
        // BGM
        BGMSetting();
        // SFX
        //SFXSetting();
    }
    private void Start()
    {
        // Load all setting from JSON
        SettingData loadedSettingData = SettingHandler.instance.LoadSetting();
        if(loadedSettingData != null)
        {
            setting_MasterSlider.value = loadedSettingData.masterVolume;
            setting_BGMSlider.value = loadedSettingData.bgmVolume;
            setting_SFXSlider.value = loadedSettingData.sfxVolume;
        }
        setting_OriginalMasterVolume = setting_MasterSlider.value;
        setting_OriginalBGMVolume = setting_BGMSlider.value;
        setting_OriginalSFXVolume = setting_SFXSlider.value;
    }
    private void Update()
    {
        // Master
        MasterSetting();
        // BGM
        BGMSetting();
        // SFX
        SFXSetting();
    }
    private void MasterSetting()
    {
        setting_MasterValue.text = Convert.ToInt32(setting_MasterSlider.value * 100).ToString();
        if (setting_MasterSlider.value != setting_OriginalMasterVolume)
        {
            setting_ConfirmButton.SetActive(true);
        }
    }
    private void BGMSetting()
    {
        if (setting_MasterSlider.value >= setting_BGMSlider.value)
        {
            setting_BGMAudioSouce.volume = setting_BGMSlider.value;
        }
        else
        {
            setting_BGMAudioSouce.volume = setting_MasterSlider.value;
        }
        if (setting_BGMSlider.value != setting_OriginalBGMVolume)
        {
            setting_ConfirmButton.SetActive(true);
        }
        setting_BGMValue.text = Convert.ToInt32(setting_BGMSlider.value * 100).ToString();
    }
    private void SFXSetting()
    {
        if (setting_MasterSlider.value >= setting_SFXSlider.value)
        {
            //sfxAudioSouce.volume = sfxSlider.value;
        }
        else
        {
            //sfxAudioSouce.volume = masterSlider.value;
        }
        if (setting_SFXSlider.value != setting_OriginalSFXVolume)
        {
            setting_ConfirmButton.SetActive(true);
        }
        setting_SFXValue.text = Convert.ToInt32(setting_SFXSlider.value * 100).ToString();
    }
    public void ConfirmSetting()
    {
        SettingHandler.instance.SaveSetting(setting_MasterSlider.value, setting_BGMSlider.value, setting_SFXSlider.value);

        setting_OriginalMasterVolume = setting_MasterSlider.value;
        setting_OriginalBGMVolume = setting_BGMSlider.value;
        setting_OriginalSFXVolume = setting_SFXSlider.value;
        setting_ConfirmButton.SetActive(false);
    }
}
