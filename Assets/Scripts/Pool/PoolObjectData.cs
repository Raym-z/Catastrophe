using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PoolObjectData : ScriptableObject
{
    public GameObject originalPrefab;
    public GameObject containerPrefab;
    public int poolID;

}
