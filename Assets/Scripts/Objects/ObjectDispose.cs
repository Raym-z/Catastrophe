using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDispose : MonoBehaviour
{
    Transform playerTransform;
    float maxDistance = 25f;


    private void Start()
    {
        if (GameManager.instance != null && GameManager.instance.playerTransform != null)
        {
            playerTransform = GameManager.instance.playerTransform;
        }
    }


    private void Update()
    {
        if (IsGamePaused())
        {
            return;
        }

        if (playerTransform == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance > maxDistance)
        {
            Destroy(gameObject);
        }
    }



    bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }
}
