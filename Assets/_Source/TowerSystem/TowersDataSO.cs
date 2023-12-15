using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace TowerSystem
{
    [Serializable]
    public class TowerData
    {
        [field: SerializeField] public TowerType TowerType { get; private set; }
        [field: SerializeField] public int Attack { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
    
    [CreateAssetMenu(fileName = "new TowerData", menuName = "SO/TowerData")]
    public class TowersDataSO : ScriptableObject
    {
        [field: SerializeField] public TowerData[] TowersData { get; private set; }
    }
}
