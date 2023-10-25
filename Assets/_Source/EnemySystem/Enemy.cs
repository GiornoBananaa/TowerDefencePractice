using System;
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
        private EnemyInvoker _enemyInvoker;
        
        public Action OnLifeEnd;
        public Action OnEnemyDestroy;
        
        private void Start()
        {
            _navMeshAgent.speed = Speed;
            _enemyInvoker = new EnemyInvoker(new EnemyMovement(_navMeshAgent));
            _enemyInvoker.SetNewEnemyTarget(Vector3.zero);
        }
        
        
        private void OnDestroy()
        {
            OnEnemyDestroy.Invoke();
        }
    }
}
