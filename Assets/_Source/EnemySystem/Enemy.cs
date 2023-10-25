using System;
using BaseSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField] public EnemyTypes EnemyType{ get; private set; }
        [field: SerializeField] public float Speed{ get; private set; }
        [field: SerializeField] public int Attack{ get; private set; }
        [field: SerializeField] public int Hp{ get; private set; }

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyTargetTrigger _enemyTargetTrigger;
        [SerializeField] private int _baseLayer;
        
        private EnemyInvoker _enemyInvoker;
        private BaseHealth _baseHealth;
        
        public Action OnLifeEnd;
        public Action OnEnemyDestroy;

        public void SetBaseHealth(BaseHealth baseHealth)
        {
            _baseHealth = baseHealth;
        }
        
        private void Start()
        {
            _navMeshAgent.speed = Speed;
            _enemyInvoker = new EnemyInvoker(new EnemyMovement(_navMeshAgent));
            _enemyInvoker.SetNewEnemyTarget(Vector3.zero);
            _enemyTargetTrigger.Construct(_enemyInvoker,_baseLayer);
        }
        
        private void OnDestroy()
        {
            OnEnemyDestroy.Invoke();
        }
    }
}
