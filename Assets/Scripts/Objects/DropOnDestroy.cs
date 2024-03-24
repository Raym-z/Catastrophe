using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DropItem
{
    public GameObject itemPrefab;
    [SerializeField]
    [Range(0f, 1f)]
    private float chance = 1f;

    public float Chance => chance;
}

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField]
    private List<DropItem> dropItemPrefab = new List<DropItem>();

    private void OnDestroy()
    {
        if (GameManager.instance == null) return;
        CheckDrop();
    }

    public void CheckDrop()
    {
        if (dropItemPrefab.Count <= 0)
        {
            Debug.LogWarning("List of drop items is empty.");
            return;
        }

        foreach (DropItem dropItem in dropItemPrefab)
        {
            if (UnityEngine.Random.value < dropItem.Chance)
            {
                GameObject toDrop = dropItem.itemPrefab;

                if (toDrop == null)
                {
                    Debug.LogWarning("DropOnDestroy: reference to dropItemPrefab is null.");
                    return;
                }
                SpawnManager.instance.SpawnObject(transform.position, toDrop);
            }
        }
    }
}
