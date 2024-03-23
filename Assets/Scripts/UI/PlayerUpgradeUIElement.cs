using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerUpgradeUIElement : MonoBehaviour
{
    [SerializeField] PlayerPersistentUpgrades upgrade;
    [SerializeField] GameObject iconContainer;
    [SerializeField] Sprite icon;
    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] UpgradeScaler Bar;
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
        if (playerUpgrades.level == 0)
        {
            if (dataContainer.coins > 50)
            {
                dataContainer.coins -= 50;
                playerUpgrades.level += 1;
                UpdateElement();
            }
        }
        else if (dataContainer.coins >= playerUpgrades.costToUpgrade * playerUpgrades.level)
        {
            dataContainer.coins -= playerUpgrades.costToUpgrade * playerUpgrades.level;
            playerUpgrades.level += 1;
            UpdateElement();
        }
    }

    void UpdateElement()
    {
        PlayerUpgrades playerUpgrades = dataContainer.upgrades[(int)upgrade];

        upgradeName.text = upgrade.ToString();
        description.text = playerUpgrades.description.ToString();
        CheckMax(playerUpgrades);
        Bar.UpdateStoreSlider(playerUpgrades.level, playerUpgrades.max_level);
        iconContainer.GetComponent<Image>().sprite = icon;
    }

    private void CheckMax(PlayerUpgrades playerUpgrades)
    {
        if (playerUpgrades.level == 0)
        {
            price.text = "50";
        }
        else if (playerUpgrades.level >= playerUpgrades.max_level)
        {
            price.text = "MAX";
        }
        else
        {
            price.text = (playerUpgrades.costToUpgrade * playerUpgrades.level).ToString();
        }
    }

}
