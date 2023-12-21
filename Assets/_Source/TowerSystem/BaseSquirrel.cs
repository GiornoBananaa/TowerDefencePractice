using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TowerSystem
{
    public class BaseSquirrel : Tower
    {
        private static readonly int _attackAnimationTriggerHash = Animator.StringToHash("Attack");
        private static readonly int _attackSpeedAnimationFloatHash = Animator.StringToHash("AttackSpeed");
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private AnimationClip _attackClip;
        [SerializeField] private float _attackTime;
        protected BulletPool _bulletPool;

        public override void Construct(TowerCell towerCell, TowerData[] towerData)
        {
            base.Construct(towerCell,towerData);
            _bulletPool = new BulletPool(_bulletPrefab,10);
            AnimationEventDispatcher.OnAnimationCustomEvent.AddListener(Attack);
        }
        
        protected override void AttackEnemy()
        {
            Animator.SetTrigger(_attackAnimationTriggerHash);
            Debug.Log(_attackClip.length + " " + TowerData.AttackCooldown + " " + _attackClip.length/TowerData.AttackCooldown);
            Animator.SetFloat(_attackSpeedAnimationFloatHash, _attackClip.length/TowerData.AttackCooldown);
        }
        
        private void Attack(string animationName)
        {
            
            if(_enemiesInRange.Count == 0) return;
            
            if (_bulletPool.TryGetFromPool(out Bullet bullet))
            {
                while (!_enemiesInRange[0].gameObject.activeSelf
                       || Vector3.Distance( transform.TransformPoint(_enemyTrigger.center), 
                           _enemiesInRange[0].transform.position) > TowerData.AttackRange * 1.5f)
                {
                    _enemiesInRange.RemoveAt(0);
                    if(_enemiesInRange.Count == 0)
                        return;
                }
                while (_enemiesInRange.Count != 0)
                {
                    if (_enemiesInRange[0].AddPredictedDamage(bullet.Damage))
                    {
                        transform.DOLookAt(_enemiesInRange[0].transform.position, 0.2f,AxisConstraint.Y);
                        bullet.SetTarget(_enemiesInRange[0], TowerData.Attack);
                        bullet.transform.position = _firePoint.position;
                        break;
                    }

                    _enemiesInRange.RemoveAt(0);
                }
            }
        }
    }
}
