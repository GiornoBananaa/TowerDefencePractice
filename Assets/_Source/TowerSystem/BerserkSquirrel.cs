using System;
using Core;
using UISystem;
using UnityEngine;

namespace TowerSystem
{
    public class BerserkSquirrel : Tower, IKillable
    {
        [SerializeField] private HealthView _healthView;
        private int _currentHp;
        private BerserkTowerData _berserkTowerData;

        public override TowerData TowerData => _berserkTowerData;
        
        public event Action OnLifeEnd;
        
        public override void Construct(TowerCell towerCell, TowerData towerData)
        {
            _berserkTowerData = ((BerserkTowerData)towerData);
            base.Construct(towerCell,towerData);
            _currentHp = _berserkTowerData.HP;
            _healthView.ChangeHeath((float)_currentHp/_berserkTowerData.HP);
        }
        
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
            
            // no attack(temporarily)
        }
        
        public void TakeDamage(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
            {
                _towerCell.EnableCell();
                Destroy(gameObject);
                OnLifeEnd?.Invoke();
            }

            if (_currentHp < 0)
                _currentHp = 0;
            _healthView.ChangeHeath((float)_currentHp/_berserkTowerData.HP);
        }

        public void Heal(int hp)
        {
            _currentHp = _currentHp + hp > _berserkTowerData.HP ?
                _berserkTowerData.HP : _currentHp + hp;
            _healthView.ChangeHeath((float)_currentHp/_berserkTowerData.HP);
        }
    }
}