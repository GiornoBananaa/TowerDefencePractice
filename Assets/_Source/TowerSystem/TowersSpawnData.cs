using System;
using UnityEngine;

namespace TowerSystem
{
    [Serializable]
    public class TowersSpawnData
    {
        [field: SerializeField] public TowerType TowerType { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}