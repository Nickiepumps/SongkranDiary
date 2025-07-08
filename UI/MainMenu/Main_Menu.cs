using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{
    [Header("Windows")]
    [SerializeField] private GameObject mainMenuWindow;
    [SerializeField] private GameObject tutorialWindow;
    [SerializeField] private GameObject settingWindow;
    public void OpenMenu()
    {
        mainMenuWindow.SetActive(true);
        settingWindow.SetActive(false);
        tutorialWindow.SetActive(false);
    }
    public void OpenTutorial()
    {
        mainMenuWindow.SetActive(false);
        settingWindow.SetActive(false);
        tutorialWindow.SetActive(true);
    }
    public void OpenSetting()
    {
        mainMenuWindow.SetActive(false);
        settingWindow.SetActive(true);
    }
}
