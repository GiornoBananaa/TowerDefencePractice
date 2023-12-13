using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

namespace TowerSystem
{
    public class BaseSquirrel : Tower
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        protected BulletPool _bulletPool;
        
        protected override void Awake()
        {
            base.Awake();
            _bulletPool = new BulletPool(_bulletPrefab,10);
        }

        protected override void AttackEnemy()
        {
            if (_bulletPool.TryGetFromPool(out Bullet bullet))
            {
                while (!_enemiesInRange[0].gameObject.activeSelf
                       || Vector3.Distance( transform.TransformPoint(_enemyTrigger.center), _enemiesInRange[0].transform.position) > TowerData.AttackRange * 1.5f)
                {
                    _enemiesInRange.RemoveAt(0);
                    if(_enemiesInRange.Count == 0)
                        return;
                }
                bullet.SetTarget(_enemiesInRange[0], TowerData.Attack);
                bullet.transform.position = _firePoint.position;
            }
        }
    }
}
