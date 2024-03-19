using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class WeaponStats
{
    public int damage;
    public float timeToAttack;
    public int numberOfAttacks;
    // public float CriticalChance;

    public WeaponStats(int damage, float timeToAttack, int numberOfAttacks)
    {
        this.damage = damage;
        this.timeToAttack = timeToAttack;
        this.numberOfAttacks = numberOfAttacks;
    }



    internal void Sum(WeaponStats weaponUpgradeStats)
    {
        this.damage += weaponUpgradeStats.damage;
        this.timeToAttack += weaponUpgradeStats.timeToAttack;
        this.numberOfAttacks += weaponUpgradeStats.numberOfAttacks;
    }

}


[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string Name;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    public List<UpgradeData> upgrades;

}
