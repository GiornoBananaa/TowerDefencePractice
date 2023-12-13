using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

namespace TowerSystem
{
    public abstract class Tower : MonoBehaviour
    {
        public TowerData TowerData { get; private set; }
        
        [SerializeField] protected SphereCollider _enemyTrigger;
        protected List<Enemy> _enemiesInRange;
        private float _timeElapsed;
        
        protected virtual void Awake()
        {
            _enemiesInRange = new List<Enemy>();
        }
        
        protected virtual void Update()
        {
            if(_enemiesInRange.Count == 0) return;
            
            CheckCooldown();
        }
        
        protected abstract void AttackEnemy();
        
        public void Construct(Vector3 attackRangePosition, TowerData towerData)
        {
            _enemyTrigger.center = transform.InverseTransformPoint(attackRangePosition);
            TowerData = towerData;
            _enemyTrigger.radius = TowerData.AttackRange;
        }
        
        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                _enemiesInRange.Add(enemy);
            }
        }
        
        protected void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                _enemiesInRange.Remove(enemy);
            }
        }
        
        private void CheckCooldown()
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed > TowerData.AttackCooldown)
            {
                _timeElapsed = 0;
                AttackEnemy();
            }
        }
    }
}