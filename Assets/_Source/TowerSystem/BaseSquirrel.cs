using System.Collections.Generic;
using DG.Tweening;
using EnemySystem;
using UnityEngine;

namespace TowerSystem
{
    public class BaseSquirrel : Tower
    {
        private static readonly int _attackAnimationTriggerHash = Animator.StringToHash("Attack");
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        protected BulletPool _bulletPool;

        public override void Construct(TowerCell towerCell, TowerData[] towerData)
        {
            base.Construct(towerCell,towerData);
            _bulletPool = new BulletPool(_bulletPrefab,10);
        }

        protected override void AttackEnemy()
        {
            if (_bulletPool.TryGetFromPool(out Bullet bullet))
            {
                Animator.SetTrigger(_attackAnimationTriggerHash);
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
