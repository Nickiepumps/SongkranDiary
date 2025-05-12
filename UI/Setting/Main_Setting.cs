using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Main_Setting : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private GameObject confirmButton;
    [Header("Sounds")]
    [SerializeField] private AudioSource bgmAudioSouce;
    [SerializeField] private AudioSource sfxAudioSouce;
    [Header("Slider")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TMP_Text masterValue;
    [SerializeField] private TMP_Text bgmValue;
    [SerializeField] private TMP_Text sfxValue;

    private float originalMasterVolume;
    private float originalBGMVolume;
    private float originalSFXVolume;
    private void OnEnable()
    {
        // Get all setting values
        originalMasterVolume = masterSlider.value;
        originalBGMVolume = bgmSlider.value;
        originalSFXVolume = sfxSlider.value;
    }
    private void OnDisable()
    {
        // Reset all setting to the original value if player doesn't confirm the setting
        if(confirmButton.activeSelf == true)
        {
            masterSlider.value = originalMasterVolume;
            bgmSlider.value = originalBGMVolume;
            sfxSlider.value = originalSFXVolume;
            confirmButton.SetActive(false);
        }
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
        masterValue.text = Convert.ToInt32(masterSlider.value * 100).ToString();
        if (masterSlider.value != originalMasterVolume)
        {
            confirmButton.SetActive(true);
        }
    }
    private void BGMSetting()
    {
        if (masterSlider.value >= bgmSlider.value)
        {
            bgmAudioSouce.volume = bgmSlider.value;
        }
        else
        {
            bgmAudioSouce.volume = masterSlider.value;
        }
        if(bgmSlider.value != originalBGMVolume)
        {
            confirmButton.SetActive(true);
        }
        bgmValue.text = Convert.ToInt32(bgmSlider.value * 100).ToString();
    }
    private void SFXSetting()
    {
        if (masterSlider.value >= sfxSlider.value)
        {
            //sfxAudioSouce.volume = sfxSlider.value;
        }
        else
        {
            //sfxAudioSouce.volume = masterSlider.value;
        }
        if (sfxSlider.value != originalSFXVolume)
        {
            confirmButton.SetActive(true);
        }
        sfxValue.text = Convert.ToInt32(sfxSlider.value * 100).ToString();
    }
    public void ConfirmSetting()
    {
        // To Do: Save all settings to JSON
        originalMasterVolume = masterSlider.value;
        originalBGMVolume = bgmSlider.value;
        originalSFXVolume = sfxSlider.value;
        confirmButton.SetActive(false);
    }

}
