using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDaggerWeapon : WeaponBase
{
    [SerializeField] GameObject ThrowingDaggerPrefab;
    [SerializeField] float spread = 0.5f;


    public override void Attack()
    {
        UpdateVectorOfAttack();
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject dagger = Instantiate(ThrowingDaggerPrefab);

            Vector3 newPosition = transform.position;
            if (weaponStats.numberOfAttacks > 1)
            {
                newPosition.y -= (spread * weaponStats.numberOfAttacks - 1) / 2;
                newPosition.y += spread * i;
            }

            dagger.transform.position = newPosition;

            ThrowingDaggerProjectile throwingDaggerProjectile = dagger.GetComponent<ThrowingDaggerProjectile>();
            throwingDaggerProjectile.SetDirection(vectorOfAttack.x, vectorOfAttack.y);
            throwingDaggerProjectile.damage = GetDamage();
            throwingDaggerProjectile.criticalChance = GetCriticalChance();

        }
    }

}
