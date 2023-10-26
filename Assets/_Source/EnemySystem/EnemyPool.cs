using System;
using System.Collections.Generic;
using BaseSystem;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace EnemySystem
{
    public class EnemyPool
    {
            private Queue<Enemy>[] _enemies;
            private List<Enemy>[] _releasedEnemies;
            private GameObject[] _enemiesPrefab;
            private int _count;
            private int _typesCount;
            private BaseHealth _baseHealth;
            private EnemyTypes[] _availableTypes;

            public Action OnReturnToSpawnPoint;
            public Action OnGoAttackBase;

            public EnemyPool(EnemyTypes[] availableTypes,GameObject[] enemiesPrefab,BaseHealth baseHealth)
            {
                _baseHealth = baseHealth;
                _enemiesPrefab = enemiesPrefab;
                _availableTypes = availableTypes;
                _typesCount = (int)EnemyTypes.NumberOfTypes;
                _releasedEnemies = new List<Enemy>[availableTypes.Length];
                _enemies = new Queue<Enemy>[availableTypes.Length];
                for (int i = 0; i < availableTypes.Length;i++)
                {
                    _enemies[i] = new Queue<Enemy>();
                    _releasedEnemies[i] = new List<Enemy>();
                }
            }
            
            public bool GetFromPool(out GameObject enemyInstance,Vector3 position, Quaternion rotation)
            {
                EnemyTypes enemyType = _availableTypes[Random.Range(0, _availableTypes.Length)];
                
                int enemyTypeInt = (int)enemyType;
                
                if (_enemies[enemyTypeInt].Count == 0)
                {
                    CreateEnemy(enemyType,position, rotation);
                }
                
                Enemy enemy = null;
                while (enemy == null)
                {
                    enemy = _enemies[enemyTypeInt].Dequeue();
                    if (_enemies.Length == 0)
                        CreateEnemy(enemyType, position, rotation);
                }
                
                enemyInstance = enemy.gameObject;
                
                _releasedEnemies[enemyTypeInt].Add(enemy);
                enemyInstance.transform.position = position;
                enemyInstance.transform.rotation = rotation;
                enemyInstance.SetActive(true);
                
                return true;
            }

            public void ReturnToPool(Enemy enemy)
            {
                int enemyType = (int)enemy.EnemyType;
                _releasedEnemies[enemyType].Remove(enemy);
                _enemies[enemyType].Enqueue(enemy);
                enemy.gameObject.SetActive(false);
            }

            public void SetAvailableTypesCount(EnemyTypes[] availableTypesCount)
            {
                _availableTypes = availableTypesCount;
            }
            
            private void CreateEnemy(EnemyTypes enemyType, Vector3 position, Quaternion rotation)
            {
                GameObject enemyInstance = Object.Instantiate(_enemiesPrefab[(int)enemyType], position, rotation);
                if (enemyInstance.TryGetComponent(out Enemy enemy))
                {
                    enemy.Construct(_baseHealth);
                    enemy.OnLifeEnd += () => ReturnToPool(enemy);
                    enemy.OnEnemyDestroy += () => _count--;
                    ReturnToPool(enemy);
                    _count++;
                    OnGoAttackBase += enemy.GoAttackBase;
                    OnReturnToSpawnPoint += enemy.GoBackToSpawn;
                }
            }
    }
}
