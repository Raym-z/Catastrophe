using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    [SerializeField] GameObject leftMeleeObject;
    [SerializeField] GameObject rightMeleeObject;

    Movement playerMoveMENT;
    [SerializeField] Vector2 attackSize = new Vector2(4f, 2f);

    private new void Awake()
    {
        playerMoveMENT = GetComponentInParent<Movement>();
    }


    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }

    IEnumerator AttackProcess()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            // Debug.Log("Attack");
            if (playerMoveMENT.lastHorizontalDeCoupledVector > 0)
            {
                rightMeleeObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightMeleeObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            else
            {
                leftMeleeObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftMeleeObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            yield return new WaitForSeconds(0.3f);
        }

    }

}

