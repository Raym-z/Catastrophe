using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] float timeToLive = 1f;
    float ttl = 1f;

    private void OnEnable()
    {
        ttl = timeToLive;
    }
    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
