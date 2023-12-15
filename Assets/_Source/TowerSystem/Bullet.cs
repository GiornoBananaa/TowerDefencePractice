using System;
using EnemySystem;
using UnityEngine;

namespace TowerSystem
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 10f;
        [SerializeField] private float _speed;
        private int _damage;
        private float _currentLifeTime;
        private Transform _target;
        private Enemy _targetEnemy;
        private BulletPool _owner;
        public Action OnLifeEnd;
        public Action OnBulletDestroy;

        public void SetTarget(Enemy enemy, int damage)
        {
            _damage = damage;
            _target = enemy.transform;
            _targetEnemy = enemy;
        }
        
        private void OnEnable()
        {
            _currentLifeTime = _lifeTime;
        }
        
        private void Update()
        {
            if(_targetEnemy == null) return;
            Move();
            CheckLifeTime();
        }

        private void Move()
        {
            if(!_target.gameObject.activeSelf)
            {
                OnLifeEnd.Invoke();
            }
            
            transform.LookAt(_target);
            transform.position = Vector3.MoveTowards(transform.position, _target.position,_speed * Time.deltaTime);
        }
        
        private void CheckLifeTime()
        {
            _currentLifeTime -= Time.deltaTime;
            if (_currentLifeTime < 0)
            {
                _currentLifeTime = _lifeTime;
                OnLifeEnd.Invoke();
            }
        }

        private void OnDestroy()
        {
            OnBulletDestroy.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_targetEnemy != null && other.gameObject == _targetEnemy.gameObject)
            {
                _targetEnemy.TakeDamage(_damage);
                OnLifeEnd.Invoke();
            }
        }
    }
}
