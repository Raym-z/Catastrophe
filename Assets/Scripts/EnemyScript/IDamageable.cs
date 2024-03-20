using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Stun(float stun);

    public void Knockback(Vector3 vector, float force, float timeWeight);

    public void TakeDamage(int damage);
}