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
        private BerserkTowerData[] _berserkTowerLevelDatas;
        
        public event Action OnLifeEnd;
        
        public override TowerData TowerData => _berserkTowerLevelDatas[Level];
        private BerserkTowerData BerserkTowerData =>_berserkTowerLevelDatas[Level];

        
        public override void Construct(TowerCell towerCell, TowerData[] towerData)
        {
            _berserkTowerLevelDatas = ((BerserkTowerData[])towerData);
            base.Construct(towerCell,towerData);
            _currentHp = BerserkTowerData.HP;
            _healthView.ChangeHeath((float)_currentHp/BerserkTowerData.HP);
            AnimationEventDispatcher.OnAnimationComplete.AddListener(Death);
            transform.rotation = Quaternion.LookRotation(transform.position);
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
            Animator.SetTrigger("Damage");
            _currentHp -= damage;
            if (_currentHp <= 0)
            {
                Animator.SetTrigger("Death");
            }

            if (_currentHp < 0)
                _currentHp = 0;
            _healthView.ChangeHeath((float)_currentHp/BerserkTowerData.HP);
        }
        
        public void Heal(int hp)
        {
            _currentHp = _currentHp + hp > BerserkTowerData.HP ?
                BerserkTowerData.HP : _currentHp + hp;
            _healthView.ChangeHeath((float)_currentHp/BerserkTowerData.HP);
        }
        
        private void Death(string animationName)
        {
            if (animationName.Substring(animationName.Length-5, 5).ToLower() != "death") return;
            TowerCell.EnableCell();
            Destroy(gameObject);
            OnLifeEnd?.Invoke();
        }
    }
}