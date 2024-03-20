using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUpgradeUIElement : MonoBehaviour
{
    [SerializeField] PlayerPersistentUpgrades upgrade;

    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI price;

    [SerializeField] DataContainer dataContainer;

    void Start()
    {
        UpdateElement();
    }

    public void Upgrade()
    {
        PlayerUpgrades playerUpgrades = dataContainer.upgrades[(int)upgrade];

        if (playerUpgrades.level >= playerUpgrades.max_level) { Debug.Log("Already Maxed"); return; }
        if (dataContainer.coins >= playerUpgrades.costToUpgrade)
        {
            dataContainer.coins -= playerUpgrades.costToUpgrade;
            playerUpgrades.level += 1;
            UpdateElement();
        }
    }

    void UpdateElement()
    {
        PlayerUpgrades playerUpgrades = dataContainer.upgrades[(int)upgrade];

        upgradeName.text = upgrade.ToString();
        level.text = "Level " + playerUpgrades.level.ToString();
        price.text = "Price : " + playerUpgrades.costToUpgrade.ToString();
    }
}
