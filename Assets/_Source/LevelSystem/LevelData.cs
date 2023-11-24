using System;
using EnemySystem;
using UnityEngine;

namespace LevelSystem
{
    [Serializable]
    public class LevelData
    {
        [field: SerializeField] public float LevelDuration { get; private set; }
        [field: SerializeField] public float EnemiesSpawnCooldown { get; private set; }
        [field: SerializeField] public EnemySpawnData[] EnemySpawnData { get; private set; }
    }
}