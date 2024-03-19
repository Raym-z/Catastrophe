using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class ThrowingDaggerProjectile : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float speed;
    public int damage;
    public float criticalChance;
    private bool crit;

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

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Time.frameCount % 6 == 0)
        {
            // change the 0.f to the radius of the bullet
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.1f);
            foreach (Collider2D c in hit)
            {
                IDamageable enemy = c.GetComponent<IDamageable>();
                crit = false;
                if (enemy != null)
                {
                    float random = Random.Range(0, 1);
                    if (random < criticalChance)
                    {
                        // Debug.Log("Critical Hit");
                        damage = (int)(damage * 2);
                        crit = true;
                    }
                    PostDamage(damage, transform.position, crit);
                    enemy.TakeDamage(damage);
                    Destroy(gameObject);
                    break;
                }
            }
        }
        ttl -= Time.deltaTime;
        if (ttl < 0f)
        {
            Destroy(gameObject);
        }
    }

    public void PostDamage(int damage, Vector3 worldPosition, bool crit)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), worldPosition, crit);
    }
}
