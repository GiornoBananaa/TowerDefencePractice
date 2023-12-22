using System;
using UnityEngine;

namespace BaseSystem
{
    public class BaseHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHp;
        private int _currentHp = 100;

        public Action<int,float> OnBaseHealthChange;
        public Action OnBaseDestroy;
        
        private void Awake()
        {
            _currentHp = _maxHp;
        }

        public void TakeDamage(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
            {
                OnBaseDestroy();
            }

            if (_currentHp < 0)
                _currentHp = 0;
            
            OnBaseHealthChange?.Invoke(_currentHp, _maxHp);
        }
        
        public void Heal(int hp)
        {
            _currentHp = _currentHp + hp > _maxHp ?
                _maxHp : _currentHp + hp;
            OnBaseHealthChange(_currentHp,_maxHp);
        }
    }
}
