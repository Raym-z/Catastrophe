using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolMember
{
    PoolMember poolMember;
    WeaponBase weapon;
    Vector3 direction;
    public float attackArea = 0.1f;
    float speed;
    int damage;
    int numOfHits = 1;
    public float criticalChance;
    private bool crit;

    List<IDamageable> enemiesHit;

    float ttl = 6f;

    public void SetDirection(float dir_x, float dir_y)
    {
        direction = new Vector3(dir_x, dir_y);

        if (dir_x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }
    void Update()
    {
        Move();

        if (Time.frameCount % 6 == 0)
        {
            HitDetection();
        }
        TimerToLive();
    }

    private void HitDetection()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, attackArea);
        foreach (Collider2D c in hit)
        {
            if (numOfHits > 0)
            {
                IDamageable enemy = c.GetComponent<IDamageable>();
                if (enemy != null)
                {
                    if (CheckRepeatHit(enemy) == false)
                    {
                        crit = false;
                        float random = Random.Range(0, 1);
                        if (random < criticalChance)
                        {
                            damage = (int)(damage * 2);
                            crit = true;
                        }
                        weapon.ApplyDamage(c.transform.position, damage, enemy, crit);
                        enemiesHit.Add(enemy);
                        numOfHits -= 1;
                    }

                }
            }
            else
            {
                break;
            }
        }
        if (numOfHits <= 0)
        {
            DestroyProjectile();
        }
    }

    private bool CheckRepeatHit(IDamageable enemy)
    {
        if (enemiesHit == null) { enemiesHit = new List<IDamageable>(); }

        return enemiesHit.Contains(enemy);
    }


    private void TimerToLive()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0f)
        {
            DestroyProjectile();
        }
    }
    private void DestroyProjectile()
    {
        if (poolMember == null)
        {
            Destroy(gameObject);
        }
        else
        {
            poolMember.ReturnToPool();
        }
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }


    public void PostDamage(int damage, Vector3 worldPosition, bool crit)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), worldPosition, crit);
    }

    public void SetStats(WeaponBase weaponBase)
    {
        weapon = weaponBase;
        damage = weaponBase.GetDamage();
        criticalChance = weaponBase.GetCriticalChance();
        numOfHits = weaponBase.weaponStats.numberOfHits;
        speed = weaponBase.weaponStats.projectileSpeed;
    }

    private void OnEnable()
    {
        ttl = 6;
    }

    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }
}
