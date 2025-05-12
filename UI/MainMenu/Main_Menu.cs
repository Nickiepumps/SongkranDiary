using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{
    [Header("Windows")]
    [SerializeField] private GameObject mainMenuWindow;
    [SerializeField] private GameObject settingWindow;
    public void OpenMenu()
    {
        mainMenuWindow.SetActive(true);
        settingWindow.SetActive(false);
    }
    public void OpenSetting()
    {
        mainMenuWindow.SetActive(false);
        settingWindow.SetActive(true);
    }
}
