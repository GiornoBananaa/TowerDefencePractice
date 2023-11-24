using System;
using UnityEngine;

namespace EnemySystem
{
    [Serializable]
    public class EnemySpawnData
    {
        [field:SerializeField] public GameObject Prefab { get; private set; }
        [field:SerializeField] public EnemyTypes EnemyType { get; private set; }
        [field:SerializeField] public int SpawnChance { get; private set; }
    }
}