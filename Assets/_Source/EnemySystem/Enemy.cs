using System;
using System.Collections.Generic;
using BaseSystem;
using TowerSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyTargetTrigger _enemyTargetTrigger;
        [SerializeField] private EnemyCollisionDetector _enemyCollisionDetector;
        [SerializeField] private int _baseLayer;
        [SerializeField] private int _towerLayer;
        [SerializeField] private int _deadlyForEnemyLayer;
        [SerializeField] private GameObject _coinPrefab;
        
        private EnemyInvoker _enemyInvoker;
        private Vector3 _destination;
        private Vector3 _spawnPoint;
        private int _predictedDamage;
        
        [field: SerializeField] public EnemyTypes EnemyType{ get; private set; }
        [field: SerializeField] public float Speed{ get; private set; }
        [field: SerializeField] public int Attack{ get; private set; }
        [field: SerializeField] public float AttackCooldown{ get; private set; }
        [field: SerializeField] public int Hp{ get; private set; }
        [field: SerializeField] public int Coins{ get; private set; }
        
        public Action OnLifeEnd;
        public Action OnReturnToPool;
        public Action OnEnemyDestroy;

        public void Construct(BaseHealth baseHealth)
        {
            _navMeshAgent.speed = Speed;
            EnemyCombat enemyCombat = new EnemyCombat(baseHealth, this);
            EnemyMovement enemyMovement = new EnemyMovement(_navMeshAgent);
            EnemyHealth enemyHealth= new EnemyHealth(Hp);
            _enemyInvoker = new EnemyInvoker(this,enemyMovement,enemyCombat,enemyHealth);
            _enemyTargetTrigger.Construct(_enemyInvoker,_baseLayer,_towerLayer);
            _enemyCollisionDetector.Construct(_enemyInvoker,_deadlyForEnemyLayer);
            _spawnPoint = transform.position;
            
            OnReturnToPool += _enemyInvoker.ResetEnemy;
            OnLifeEnd += DropCoins;
            OnLifeEnd += _enemyInvoker.ReturnToPool;
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
            OnEnemyDestroy?.Invoke();
            OnReturnToPool -= _enemyInvoker.ResetEnemy;
            OnLifeEnd -= DropCoins;
            OnLifeEnd -= OnReturnToPool;
        }
        
        public void TakeDamage(int damage)
        {
            AudioManager.Instance.Play("hit_beaver");
            _enemyInvoker.TakeDamage(damage);
        }

        public bool AddPredictedDamage(int damage)
        {
            if (_predictedDamage >= Hp)
                return false;
            _predictedDamage += damage;
            return true;
        }
        
        public void GoBackToSpawn()
        {
            if(gameObject.activeInHierarchy)
            {
                _enemyInvoker.SetNewTargetPosition(_spawnPoint);
                _enemyInvoker.EnableAdditionalMoveTargeting(false);
            }
            _destination = _spawnPoint;
        }
        
        public void GoAttackBase()
        {
            if(gameObject.activeInHierarchy)
                _enemyInvoker.SetNewTargetPosition(Vector3.zero);
            _destination = Vector3.zero;
        }
        
        public void DropCoins()
        {
            for (int i =0; i < Coins; i++)
            {
                Instantiate(_coinPrefab,transform.position,transform.rotation);
            }
        }
        
    }
}
