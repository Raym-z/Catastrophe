using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterAttack : MonoBehaviour
{
    [SerializeField] float timeToDisable = 0.2f;
    float timer;

    private void OnEnable()
    {
        timer = timeToDisable;
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            gameObject.SetActive(false);
            // Debug.Log("Disabled");
        }
    }
}


