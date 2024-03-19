using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DirectionOfAttack
{
    None, Forward, LeftRight, UpDown
}

public abstract class WeaponBase : MonoBehaviour
{
    Movement playerMove;
    public WeaponData weaponData;

    public WeaponStats weaponStats;

    public float timer;
    private bool crit;

    Character wielder;
    public Vector2 vectorOfAttack;
    [SerializeField] DirectionOfAttack attackDirection;

    public void Awake()
    {
        playerMove = GetComponentInParent<Movement>();
    }
    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            Attack();
            timer = weaponStats.timeToAttack;
        }
    }

    public void ApplyDamage(Collider2D[] colliders)
    {
        int damage = GetDamage();
        float criticalChance = GetCriticalChance();
        for (int i = 0; i < colliders.Length; i++)
        {
            crit = false;
            IDamageable temp = colliders[i].GetComponent<IDamageable>();
            if (temp != null)
            {
                // random 
                float random = UnityEngine.Random.Range(0, 1);
                if (random < criticalChance)
                {
                    Debug.Log("Critical Hit");
                    damage = (int)(damage * 2);
                    crit = true;
                }
                PostDamage(damage, colliders[i].transform.position, crit);
                temp.TakeDamage(damage);
            }
        }
    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;

        weaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack, wd.stats.numberOfAttacks);
    }

    public abstract void Attack();

    public int GetDamage()
    {
        int damage = (int)(weaponData.stats.damage * wielder.damageBonus);
        return damage;
    }

    public float GetCriticalChance()
    {
        float criticalChance = wielder.CriticalChance;
        return criticalChance;
    }
    public virtual void PostDamage(int damage, Vector3 targetPosition, bool crit)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition, crit);
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }

    public void AddOwnerCharacter(Character character)
    {
        wielder = character;
    }

    public void UpdateVectorOfAttack()
    {
        if (attackDirection == DirectionOfAttack.None)
        {
            vectorOfAttack = Vector2.zero;
            return;
        }

        switch (attackDirection)
        {
            case DirectionOfAttack.Forward:
                vectorOfAttack.x = playerMove.lastHorizontalCoupledVector;
                vectorOfAttack.y = playerMove.lastVerticalCoupledVector;
                break;
            case DirectionOfAttack.LeftRight:
                vectorOfAttack.x = playerMove.lastHorizontalDeCoupledVector;
                vectorOfAttack.y = 0f;
                break;
            case DirectionOfAttack.UpDown:
                vectorOfAttack.x = 0f;
                vectorOfAttack.y = playerMove.lastVerticalDeCoupledVector;
                break;
        }
        vectorOfAttack = vectorOfAttack.normalized;
    }
}

