using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingHandler : MonoBehaviour
{
    public static SettingHandler instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveSetting(float masterValue, float bgmValue, float sfxValue)
    {
        SettingData settingData = new SettingData();
        settingData.masterVolume = masterValue;
        settingData.bgmVolume = bgmValue;
        settingData.sfxVolume = sfxValue;
        string settingJson = JsonUtility.ToJson(settingData);
        Debug.Log(settingJson);
        File.WriteAllText(Application.dataPath + "/setting.json", settingJson);
    }
    public SettingData LoadSetting()
    {
        if(File.Exists(Application.dataPath + "/setting.json") == false)
        {
            return null;
        }
        string loadedSettingJson = File.ReadAllText(Application.dataPath + "/setting.json");
        SettingData loadedSetting = JsonUtility.FromJson<SettingData>(loadedSettingJson);
        Debug.Log(loadedSetting.masterVolume);
        return loadedSetting;
    }
}
