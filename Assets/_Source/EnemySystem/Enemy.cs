using System;
using BaseSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyTargetTrigger _enemyTargetTrigger;
        [SerializeField] private int _baseLayer;
        
        private EnemyInvoker _enemyInvoker;
        private EnemyCombat _enemyCombat;
        private BaseHealth _baseHealth;
        
        [field: SerializeField] public EnemyTypes EnemyType{ get; private set; }
        [field: SerializeField] public float Speed{ get; private set; }
        [field: SerializeField] public int Attack{ get; private set; }
        [field: SerializeField] public int AttackCooldown{ get; private set; }
        [field: SerializeField] public int Hp{ get; private set; }
        
        public Action OnLifeEnd;
        public Action OnEnemyDestroy;

        public void Construct(BaseHealth baseHealth)
        {
            _baseHealth = baseHealth;
            _enemyCombat = new EnemyCombat(_baseHealth, this);
            _navMeshAgent.speed = Speed;
            _enemyInvoker = new EnemyInvoker(new EnemyMovement(_navMeshAgent),_enemyCombat);
            _enemyTargetTrigger.Construct(_enemyInvoker,_baseLayer);
        }

        private void Start()
        {
            _enemyInvoker.SetNewEnemyTarget(Vector3.zero);
        }
        
        private void Update()
        {
            _enemyInvoker.UpdateCooldown();
        }

        private void OnDestroy()
        {
            OnEnemyDestroy.Invoke();
        }
    }
}
