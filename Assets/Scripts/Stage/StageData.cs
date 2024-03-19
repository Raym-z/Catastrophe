using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageEventType
{
    SpawnEnemy,
    SpawnEnemyBoss,
    SpawnObject,
    WinStage
}

[Serializable]
public class StageEvent
{
    public StageEventType eventType;
    public float time;
    public string message;
    public EnemyData enemyToSpawn;
    public GameObject objectToSpawn;
    public int count;

    public bool isRepeatedEvent;
    public float repeatEverySeconds;
    public int repeatCount;
}




[CreateAssetMenu]

public class StageData : ScriptableObject
{
    public List<StageEvent> stageEvents;
}
