using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public DataContainer dataContainer;

    private void Awake()
    {
        // PlayerPrefs.DeleteAll();
        LoadGame();
    }

    public void LoadGame()
    {
        // Load coins
        dataContainer.coins = PlayerPrefs.GetInt("Coins", 0);

        foreach (PlayerUpgrades upgrade in dataContainer.upgrades)
        {
            upgrade.level = PlayerPrefs.GetInt("Upgrade_" + upgrade.persistentUpgrades.ToString() + "_Level", 0);
        }
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


