using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

namespace TowerSystem
{
    public abstract class Tower : MonoBehaviour
    {
        public virtual TowerData TowerData { get; protected set; }
        
        [SerializeField] protected SphereCollider _enemyTrigger;
        protected List<Enemy> _enemiesInRange;
        protected TowerCell _towerCell;
        private float _timeElapsed;
        
        public virtual void Construct(TowerCell towerCell, TowerData towerData)
        {
            _towerCell = towerCell;
            _enemyTrigger.center = transform.InverseTransformPoint(towerCell.AttackRangePoint);
            TowerData = towerData;
            _enemyTrigger.radius = TowerData.AttackRange;
            _enemiesInRange = new List<Enemy>();
        }
        
        protected virtual void Update()
        {
            if(_enemiesInRange.Count == 0) return;
            
            CheckCooldown();
        }
        
        protected abstract void AttackEnemy();
        
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