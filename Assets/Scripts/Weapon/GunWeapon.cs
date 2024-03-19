using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : WeaponBase
{
    [SerializeField] GameObject bulletPrefab;
    public override void Attack()
    {
        UpdateVectorOfAttack();
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject dagger = Instantiate(bulletPrefab);

            Vector3 newPosition = transform.position;
            dagger.transform.position = newPosition;

            ThrowingDaggerProjectile throwingDaggerProjectile = dagger.GetComponent<ThrowingDaggerProjectile>();
            throwingDaggerProjectile.SetDirection(vectorOfAttack.x, vectorOfAttack.y);
            throwingDaggerProjectile.damage = GetDamage();
            throwingDaggerProjectile.criticalChance = GetCriticalChance();

        }

    }
}
