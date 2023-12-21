using System.Collections.Generic;
using Core;
using EnemySystem;
using UnityEngine;

namespace TowerSystem
{
    public abstract class Tower : MonoBehaviour
    {
        public TowerData[] TowerLevelDatas { get; protected set; }
        public virtual TowerData TowerData => TowerLevelDatas[Level];
        public int Level { get; protected set; }
        public TowerCell TowerCell { get; private set; }
        
        [field: SerializeField] public Animator Animator{ get; private set; }
        [field: SerializeField] public AnimationEventDispatcher AnimationEventDispatcher{ get; private set; }
        
        [SerializeField] protected SphereCollider _enemyTrigger;
        protected List<Enemy> _enemiesInRange;
        private float _timeElapsed;
        
        public virtual void Construct(TowerCell towerCell, TowerData[] towerData)
        {
            TowerCell = towerCell;
            _enemyTrigger.center = transform.InverseTransformPoint(towerCell.AttackRangePoint);
            TowerLevelDatas = towerData;
            _enemyTrigger.radius = TowerData.AttackRange;
            _enemiesInRange = new List<Enemy>();
        }
        
        protected virtual void Update()
        {
            if(_enemiesInRange.Count == 0) return;
            
            CheckCooldown();
        }
        
        protected abstract void AttackEnemy();

        public void UpgradeTower(int level)
        {
            Level = level;
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