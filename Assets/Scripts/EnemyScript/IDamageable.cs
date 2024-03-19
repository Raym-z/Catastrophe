using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Stun(float stun);

    public void TakeDamage(int damage);
}