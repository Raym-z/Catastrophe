using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    public int hp = 4;
    public int damage = 1;
    public float movementSpeed = 1f;

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.damage = stats.damage;
        this.movementSpeed = stats.movementSpeed;
    }

    internal void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.damage = (int)(damage * progress);
    }
}

public class Enemy : MonoBehaviour, IDamageable
{
    Transform targetDestination;
    Character targetCharacter;
    GameObject targetGameObject;

    Rigidbody2D rgdbd2d;

    public EnemyStats stats;
    [SerializeField] EnemyData enemyData;
    float stunned;
    UnityEngine.Vector3 knockbackVector;
    float knockbackForce;
    float knockbackTimeWeight;

    private void Awake()
    {
        rgdbd2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (enemyData != null)
        {
            InitSprite(enemyData.animatedPrefab);
            SetStats(enemyData.stats);
            SetTarget(GameManager.instance.playerTransform.gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }

    private void FixedUpdate()
    {
        ProcessStun();
        Move();
    }

    private void ProcessStun()
    {
        if (stunned > 0f)
        {
            stunned -= Time.fixedDeltaTime;
        }
    }


    private void Move()
    {
        if (stunned > 0f) { return; }
        UnityEngine.Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgdbd2d.velocity = CalculateMovementVelocity(direction) + CalculateKnockback();
    }

    private UnityEngine.Vector3 CalculateMovementVelocity(UnityEngine.Vector3 direction)
    {
        return direction * stats.movementSpeed * (stunned > 0f ? 0f : 1f);
    }


    public UnityEngine.Vector3 CalculateKnockback()
    {
        if (knockbackTimeWeight > 0f)
        {
            knockbackTimeWeight -= Time.fixedDeltaTime;
        }
        return knockbackVector * knockbackForce * (knockbackTimeWeight > 0f ? 1f : 0f);
    }

    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            Attack();
        }
    }

    internal void TakeDamage(object damage)
    {
        throw new NotImplementedException();
    }

    private void Attack()
    {
        if (targetCharacter == null)
        {
            targetCharacter = targetGameObject.GetComponent<Character>();
        }

        targetCharacter.TakeDamage(stats.damage);
    }

    public void TakeDamage(int damage)
    {
        stats.hp -= damage;

        if (stats.hp < 1)
        {
            GetComponent<DropOnDestroy>().CheckDrop();
            Destroy(gameObject);
        }
    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }

    internal void InitSprite(GameObject animatedPrefab)
    {
        GameObject spriteObject = Instantiate(animatedPrefab);
        spriteObject.transform.parent = transform;
        spriteObject.transform.localPosition = UnityEngine.Vector3.zero;
    }

    public void Stun(float stun)
    {
        stunned = stun;
    }

    public void Knockback(UnityEngine.Vector3 vector, float force, float timeWeight)
    {
        knockbackVector = vector;
        knockbackForce = force;
        knockbackTimeWeight = timeWeight;
    }
}
