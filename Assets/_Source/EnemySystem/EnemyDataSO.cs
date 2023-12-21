using System;
using UnityEngine;

namespace EnemySystem
{
    [CreateAssetMenu(fileName = "new EnemyData", menuName = "SO/EnemyData")]
    public class EnemyDataSO : ScriptableObject
    {
        [SerializeField] private EnemyTypes EnemyType;
        [SerializeField] private float Speed;
        [SerializeField] private int Attack;
        [SerializeField] private float AttackCooldown;
        [SerializeField] private int Hp;
        [SerializeField] private int Coins;
        [SerializeField] private GameObject Prefab;
    }
}