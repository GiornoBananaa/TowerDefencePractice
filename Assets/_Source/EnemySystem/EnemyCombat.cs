using BaseSystem;
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
        
        public EnemyCombat(BaseHealth baseHealth, Enemy enemy)
        {
            _attack = enemy.Attack;
            _attackCooldown = enemy.AttackCooldown;
            _baseHealth = baseHealth;
            _baseInAttackRange = false;
            _timeElapsed = 0;
        }
        
        public void StartBaseAttack()
        {
            _baseInAttackRange = true;
        }

        public void UpdateCooldown()
        {
            if(!_baseInAttackRange) return;
            
            _timeElapsed += Time.deltaTime;

            if (_timeElapsed > _attackCooldown)
            {
                _timeElapsed = 0;
                Attack();
            }
        }
        
        public void StopBaseAttack()
        {
            _baseInAttackRange = false;
        }

        private void Attack()
        {
            _baseHealth.TakeDamage(_attack);
        }
    }
}
