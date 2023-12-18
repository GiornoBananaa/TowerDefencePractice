using System;
using Core;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyHealth: IKillable
    {
        private readonly int _maxHp;
        private int _currentHp;

        public event Action OnLifeEnd;
        
        public EnemyHealth(int maxHp)
        {
            _maxHp = maxHp;
            _currentHp = maxHp;
        }

        public int HP => _currentHp;
        

        public void TakeDamage(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
            {
                OnLifeEnd?.Invoke();
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
