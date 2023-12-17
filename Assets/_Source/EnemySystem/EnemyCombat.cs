using System.Collections.Generic;
using System.Linq;
using BaseSystem;
using Core;
using TowerSystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyCombat
    {
        private readonly int _attack;
        private readonly float _attackCooldown;
        private float _timeElapsed;
        private bool _baseInAttackRange;
        private BaseHealth _baseHealth;
        private List<Tower> _towersInRange;
        
        public EnemyCombat(BaseHealth baseHealth, Enemy enemy)
        {
            _attack = enemy.Attack;
            _attackCooldown = enemy.AttackCooldown;
            _baseHealth = baseHealth;
            _baseInAttackRange = false;
            _timeElapsed = 0;
            _towersInRange = new List<Tower>();
        }
        
        public void StartBaseAttack()
        {
            _baseInAttackRange = true;
        }

        public void UpdateCooldown()
        {
            if(!_baseInAttackRange && _towersInRange.Count == 0) return;
            
            _timeElapsed += Time.deltaTime;
            
            if (_timeElapsed > _attackCooldown)
            {
                _timeElapsed = 0;
                
                if(_towersInRange.Count > 0)
                    AttackTower();
                else
                    AttackBase();
            }
        }
        
        public void StopBaseAttack()
        {
            _baseInAttackRange = false;
        }
        
        public void StartTowerAttack(Tower tower)
        {
            _towersInRange.Add(tower);
        }
        
        public void StopTowerAttack(Tower tower)
        {
            _towersInRange.Remove(tower);
        }
        
        private void AttackBase()
        {
            _baseHealth.TakeDamage(_attack);
        }
        
        private void AttackTower()
        {
            Tower tower = _towersInRange.First();
            if (tower == null)
            {
                _towersInRange.Remove(tower);
            }
            else
            {
                ((IKillable)tower).TakeDamage(_attack);
            }
        }
    }
}
