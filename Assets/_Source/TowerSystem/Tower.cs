using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

namespace TowerSystem
{
    public class Tower : MonoBehaviour
    {
        [field: SerializeField] public int Attack { get; private set; }
        [field: SerializeField] public int BulletSpeed { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private SphereCollider _enemyTrigger;
        
        private BulletPool _bulletPool;
        private List<Enemy> _enemiesInRange;
        private float _timeElapsed;

        private void Awake()
        {
            _enemyTrigger.radius = Radius;
            _bulletPool = new BulletPool(_bulletPrefab,10);
            _enemiesInRange = new List<Enemy>();
        }

        private void Update()
        {
            if(_enemiesInRange.Count == 0) return;
            
            CheckCooldown();
        }

        private void CheckCooldown()
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed > AttackCooldown)
            {
                _timeElapsed = 0;
                AttackEnemy();
            }
        }

        private void AttackEnemy()
        {
            if (_bulletPool.TryGetFromPool(out Bullet bullet))
            {
                while (!_enemiesInRange[0].gameObject.activeSelf
                       || Vector3.Distance( transform.TransformPoint(_enemyTrigger.center), _enemiesInRange[0].transform.position) > Radius * 1.5f)
                {
                    _enemiesInRange.RemoveAt(0);
                    if(_enemiesInRange.Count == 0)
                        return;
                }
                bullet.SetTarget(_enemiesInRange[0], Attack, BulletSpeed);
                bullet.transform.position = _firePoint.position;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                _enemiesInRange.Add(enemy);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                _enemiesInRange.Remove(enemy);
            }
        }

        public void SetRangePoint(Vector3 position)
        {
            _enemyTrigger.center = transform.InverseTransformPoint(position);
        }
    }
}
