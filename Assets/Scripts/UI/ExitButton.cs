using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{

    [SerializeField] DataContainer dataContainer;
    public void QuitApplication()
    {
        SaveGame();
        Debug.Log("Quiting Application");
        Application.Quit();
    }

    public void SaveGame()
    {
        if (dataContainer == null)
        {
            Debug.LogError("DataContainer reference is null!");
            return;
        }
        // Save coins
        PlayerPrefs.SetInt("Coins", dataContainer.coins);

        // Save upgrade levels
        foreach (PlayerUpgrades upgrade in dataContainer.upgrades)
        {
            PlayerPrefs.SetInt("Upgrade_" + upgrade.persistentUpgrades.ToString() + "_Level", upgrade.level);
        }
        PlayerPrefs.Save();
        Debug.Log("Game saved!");
    }
}
