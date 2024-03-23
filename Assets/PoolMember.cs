using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolMember : MonoBehaviour
{
    ObjectPool pool;
    private Quaternion originalRotation;
    public void Set(ObjectPool pool)
    {
        this.pool = pool;
        GetComponent<IPoolMember>().SetPoolMember(this);
    }

    public void ReturnToPool()
    {
        pool.ReturnToPool(gameObject);
    }
}
