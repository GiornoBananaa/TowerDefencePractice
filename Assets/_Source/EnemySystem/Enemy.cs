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
        [SerializeField] private GameObject _coinPrefab;
        
        private EnemyInvoker _enemyInvoker;
        private Vector3 _destination;
        private Vector3 _spawnPoint;
        
        [field: SerializeField] public EnemyTypes EnemyType{ get; private set; }
        [field: SerializeField] public float Speed{ get; private set; }
        [field: SerializeField] public int Attack{ get; private set; }
        [field: SerializeField] public int AttackCooldown{ get; private set; }
        [field: SerializeField] public int Hp{ get; private set; }
        [field: SerializeField] public int Coins{ get; private set; }
        
        public Action OnLifeEnd;
        public Action OnEnemyDestroy;

        public void Construct(BaseHealth baseHealth)
        {
            _navMeshAgent.speed = Speed;
            EnemyCombat enemyCombat = new EnemyCombat(baseHealth, this);
            EnemyMovement enemyMovement = new EnemyMovement(_navMeshAgent);
            EnemyHealth enemyHealth= new EnemyHealth(Hp,this);
            _enemyInvoker = new EnemyInvoker(this,enemyMovement,enemyCombat,enemyHealth);
            _enemyTargetTrigger.Construct(_enemyInvoker,_baseLayer);

            _spawnPoint = transform.position;
            
            OnLifeEnd += _enemyInvoker.ResetEnemy;
            OnLifeEnd += DropCoins;
        }

        private void OnEnable()
        {
            if (_enemyInvoker == null) return;
            _enemyInvoker.ResetEnemy();
            _enemyInvoker.SetNewTargetPosition(_destination);
        }

        private void Start()
        {
            _enemyInvoker.ResetEnemy();
        }

        private void Update()
        {
            _enemyInvoker.UpdateAttackCooldown();
        }

        private void OnDestroy()
        {
            OnEnemyDestroy.Invoke();
        }
        
        public void TakeDamage(int damage)
        {
            _enemyInvoker.TakeDamage(damage);
        }
        
        public void GoBackToSpawn()
        {
            if(gameObject.activeInHierarchy)
                _enemyInvoker.SetNewTargetPosition(_spawnPoint);
            _destination = _spawnPoint;
        }
        
        public void GoAttackBase()
        {
            if(gameObject.activeInHierarchy)
                _enemyInvoker.SetNewTargetPosition(Vector3.zero);
            _destination = Vector3.zero;
        }
        
        private void DropCoins()
        {
            for (int i =0; i < Coins; i++)
            {
                Instantiate(_coinPrefab,transform.position,transform.rotation);
            }
        }
    }
}
