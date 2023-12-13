using Core;
using UnityEngine;

namespace TowerSystem
{
    public class BerserkSquirrel : Tower, IKillable
    {
        private int _currentHp;
        private int _maxHp;
        
        public int HP { get; }
        
        protected override void AttackEnemy()
        {
            while (!_enemiesInRange[0].gameObject.activeSelf
                   || Vector3.Distance( transform.TransformPoint(_enemyTrigger.center), 
                       _enemiesInRange[0].transform.position) > TowerData.AttackRange * 1.5f)
            {
                _enemiesInRange.RemoveAt(0);
                if(_enemiesInRange.Count == 0)
                    return;
            }
            
            
        }
        
        public void TakeDamage(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
            {
                // dead
            }

            if (_currentHp < 0)
                _currentHp = 0;
        }

        public void Heal(int hp)
        {
            _currentHp = _currentHp + hp > _maxHp ?
                _maxHp : _currentHp + hp;
        }
    }
}