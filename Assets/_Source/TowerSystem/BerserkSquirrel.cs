using System;
using Core;
using UnityEngine;

namespace TowerSystem
{
    public class BerserkSquirrel : Tower, IKillable
    {
        private int _currentHp;
        private BerserkTowerData _berserkTowerData;

        public override TowerData TowerData => _berserkTowerData;
        
        public event Action OnLifeEnd;
        public int HP { get; private set; }
        
        public override void Construct(TowerCell towerCell, TowerData towerData)
        {
            _berserkTowerData = ((BerserkTowerData)towerData);
            base.Construct(towerCell,towerData);
            HP = _berserkTowerData.HP;
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
        }

        public void Heal(int hp)
        {
            _currentHp = _currentHp + hp > _berserkTowerData.HP ?
                _berserkTowerData.HP : _currentHp + hp;
        }
    }
}