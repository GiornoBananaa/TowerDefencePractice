using System.Collections.Generic;
using UnityEngine;

namespace TowerSystem
{
    public class BulletPool
    {
        private Queue<Bullet> _bullets;
        private List<Bullet> _releasedBullets;
        private GameObject _bulletPrefab;
        private int _count;
        private int _poolSize;
        

        public BulletPool(GameObject bulletPrefab, int poolSize)
        {
            _bulletPrefab = bulletPrefab;
            _poolSize = poolSize;
            _releasedBullets = new List<Bullet>();
            _bullets = new Queue<Bullet>();
        }
        
        public bool TryGetFromPool(out Bullet bullet)
        {
            if (_bullets.Count == 0)
            {
                if (_count < _poolSize)
                {
                    CreateBullet();
                }
                else if (_count >= _poolSize)
                {
                    bullet = null;
                    return false;
                }
            }

            bullet = null;
            while (bullet == null)
            {
                bullet = _bullets.Dequeue();
            }
            _releasedBullets.Add(bullet);
            bullet.gameObject.SetActive(true);
            return true;
        }

        public void ReturnToPool(Bullet bullet)
        {
            _releasedBullets.Remove(bullet);
            _bullets.Enqueue(bullet);
            bullet.gameObject.SetActive(false);
        }

        private void CreateBullet()
        {
            GameObject bulletInstance = Object.Instantiate(_bulletPrefab);
            if (bulletInstance.TryGetComponent(out Bullet bullet))
            {
                bullet.OnLifeEnd += () => ReturnToPool(bullet);
                bullet.OnBulletDestroy += () => _count--;
                ReturnToPool(bullet);
                _count++;
            }
        }
    }
}