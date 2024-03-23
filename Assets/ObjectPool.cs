using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    PoolObjectData originalPoolData;

    List<GameObject> pool;
    public void Set(PoolObjectData pod)
    {
        pool = new List<GameObject>();
        originalPoolData = pod;
    }

    public void InstantiateObject()
    {
        GameObject newObject = Instantiate(originalPoolData.originalPrefab, transform);
        GameObject mainObject = newObject;
        if (originalPoolData.containerPrefab != null)
        {
            GameObject container = Instantiate(originalPoolData.containerPrefab);
            newObject.transform.SetParent(container.transform);
            newObject.transform.localPosition = Vector3.zero;
            mainObject = container;
        }
        pool.Add(mainObject);
        PoolMember poolMember = mainObject.AddComponent<PoolMember>();
        poolMember.SetDefaultRotation(mainObject.transform.rotation);
        poolMember.Set(this);
    }

    public GameObject GetObject()
    {
        if (pool.Count <= 0)
        {
            InstantiateObject();
        }
        GameObject go = pool[0];
        pool.RemoveAt(0);
        go.SetActive(true);
        return go;
    }

    public void ReturnToPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        pool.Add(gameObject);
    }
}
