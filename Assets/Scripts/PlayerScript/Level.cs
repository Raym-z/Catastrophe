using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    int experience = 0;
    public int level = 1;
    StageProgress stageProgress;

    [SerializeField] EXPBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanel;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;
    [SerializeField] UpgradeData defaultUpgrade;

    [SerializeField] List<UpgradeData> acquiredUpgrades;

    WeaponManager weaponManager;
    PassiveItems passiveItems;
    [SerializeField] List<UpgradeData> upgradesAvailableOnStart;

    [SerializeField] private AudioClip characterLevelUpSoundClip;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        passiveItems = GetComponent<PassiveItems>();
        stageProgress = FindObjectOfType<StageProgress>();
    }

    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    internal void AddUpgradesIntoTheListOfAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        if (upgradesToAdd == null) { return; }
        this.upgrades.AddRange(upgradesToAdd);
    }



    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
        AddUpgradesIntoTheListOfAvailableUpgrades(upgradesAvailableOnStart);
    }

    public void AddExperience(int amount)
    {
        experience += amount * (int)stageProgress.EXPMultiplier;
        CheckLevelUp();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    public void Upgrade(int selectedUpgradeID)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeID];

        if (acquiredUpgrades == null)
        {
            acquiredUpgrades = new List<UpgradeData>();
        }
        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponGet:
                weaponManager.AddWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemGet:
                passiveItems.Equip(upgradeData.item);
                AddUpgradesIntoTheListOfAvailableUpgrades(upgradeData.item.upgrades);
                break;
            case UpgradeType.ItemUpgrade:
                passiveItems.UpgradeItem(upgradeData);
                break;

        }
        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    public void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        SFXManager.instance.PlaySoundFXClip(characterLevelUpSoundClip, transform, 1f);
        if (selectedUpgrades == null)
        {
            selectedUpgrades = new List<UpgradeData>();
        }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(3));
        upgradePanel.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        experienceBar.SetLevelText(level);
    }

    public void ShuffleUpgrades()
    {
        for (int i = upgrades.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            UpgradeData shuffleElement = upgrades[i];
            upgrades[i] = upgrades[j];
            upgrades[j] = shuffleElement;
        }
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        ShuffleUpgrades();
        List<UpgradeData> upgradeList = new List<UpgradeData>();
        List<int> History = new List<int>();
        int x = -1;

        if (count > upgrades.Count)
        {
            // upgradeList.Add(defaultUpgrade);
            count = upgrades.Count;
        }
        for (int i = 0; i < count; i++)
        {
            while (History.Contains(x) || x == -1)
            {
                x = UnityEngine.Random.Range(0, upgrades.Count);
            }
            History.Add(x);
            upgradeList.Add(upgrades[x]);
        }
        return upgradeList;
    }
}
