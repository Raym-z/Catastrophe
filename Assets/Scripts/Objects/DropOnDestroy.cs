using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] List<GameObject> dropItemPrefab;
    [SerializeField][Range(0f, 1f)] float chance = 1f;

    private void OnDestroy()
    {
        // check if game is still on  going
        if (GameManager.instance == null) return;
        CheckDrop();
    }
    public void CheckDrop()
    {
        if (dropItemPrefab.Count <= 0)
        {
            Debug.LogWarning("List of drop item is empty.");
            return;
        }

        if (Random.value < chance)
        {
            GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];

            if (toDrop == null)
            {
                Debug.LogWarning("DropOnDestroy: reference to dropItemPrefab is null.");
                return;
            }
            SpawnManager.instance.SpawnObject(transform.position, toDrop);
        }
    }
}
