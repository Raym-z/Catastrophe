using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PauseManager pauseManager;
    [SerializeField] TextMeshProUGUI caption;
    [SerializeField] List<UpgradeButton> upgradeButtons;
    [SerializeField] List<TextMeshProUGUI> upgradeDescriptions;

    [SerializeField] List<TextMeshProUGUI> upgradeNames;

    Level characterLevel;

    List<UpgradeData> upgradeData;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
        characterLevel = GameManager.instance.playerTransform.GetComponent<Level>();
    }

    private void Start()
    {
        HideButtons();
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        pauseManager.PauseGame();
        panel.SetActive(true);

        this.upgradeData = upgradeDatas;
        caption.text = "Level UP : " + (1 + characterLevel.level).ToString();

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeDescriptions[i].gameObject.SetActive(true);
            upgradeNames[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
            ShowDescription(i);
        }
    }

    public void Clean()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
            upgradeDescriptions[i].text = "";
            upgradeNames[i].text = "";
        }
    }

    public void Upgrade(int pressedButtonID)
    {
        characterLevel.Upgrade(pressedButtonID);
        ClosePanel();
    }

    private void ShowDescription(int i)
    {
        Set(upgradeData[i], upgradeNames[i], upgradeDescriptions[i]);
    }


    public void ClosePanel()
    {
        HideButtons();
        pauseManager.UnPauseGame();
        panel.SetActive(false);
    }

    public void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
            upgradeDescriptions[i].gameObject.SetActive(false);
            upgradeNames[i].gameObject.SetActive(false);
        }
    }

    public void Set(UpgradeData upgradeData, TextMeshProUGUI upgradeNameText, TextMeshProUGUI upgradeDescriptionText)
    {
        upgradeNameText.text = upgradeData.name;
        upgradeDescriptionText.text = upgradeData.description;
    }
}
