using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerPersistentUpgrades
{
    HP, Damage, Regeneration, Speed, Armor, CriticalChance
}

[Serializable]
public class PlayerUpgrades
{
    public PlayerPersistentUpgrades persistentUpgrades;
    public int level = 0;
    public int max_level = 10;
    public int costToUpgrade = 100;
    public string description;
}

[CreateAssetMenu]
public class DataContainer : ScriptableObject
{
    public int coins;

    public List<bool> stageCompletion;

    public List<PlayerUpgrades> upgrades;
    public CharacterData selectedCharacter;
    public void stageComplete(int i)
    {
        stageCompletion[i] = true;
    }

    public int GetUpgradeLevel(PlayerPersistentUpgrades persistentUpgrade)
    {
        return upgrades[(int)persistentUpgrade].level;
    }

    public void SetSelectedCharacter(CharacterData character)
    {
        selectedCharacter = character;
    }
}
