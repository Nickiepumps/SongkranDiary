using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{
    [Header("Windows")]
    [SerializeField] private GameObject mainMenuWindow;
    [SerializeField] private GameObject albumWindow;
    [SerializeField] private GameObject settingWindow;
    [Header("Buttons")]
    [SerializeField] private GameObject continueBtn;
    private void Start()
    {
        if(PlayerDataHandler.instance.hasPlayerData == true)
        {
            continueBtn.SetActive(true);
        }
        else
        {
            continueBtn.SetActive(false);
        }
    }
    public void OpenMenu()
    {
        mainMenuWindow.SetActive(true);
        settingWindow.SetActive(false);
        albumWindow.SetActive(false);
    }
    public void OpenAlbum()
    {
        mainMenuWindow.SetActive(false);
        settingWindow.SetActive(false);
        albumWindow.SetActive(true);
    }
    public void OpenSetting()
    {
        mainMenuWindow.SetActive(false);
        settingWindow.SetActive(true);
    }
}
