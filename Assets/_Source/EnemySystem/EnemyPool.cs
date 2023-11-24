using System;
using System.Collections.Generic;
using BaseSystem;
using LevelSystem;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace EnemySystem
{
    public class EnemyPool
    {
            private int _count;
            private int _typesCount;
            private int _chancesSum;
            private Queue<Enemy>[] _enemies;
            private List<Enemy>[] _releasedEnemies;
            private BaseHealth _baseHealth;
            private EnemySpawnData[] _enemySpawnDatas;

            public Action OnReturnToSpawnPoint;
            public Action OnGoAttackBase;

            public EnemyPool(BaseHealth baseHealth)
            {
                _baseHealth = baseHealth;
                _typesCount = (int)EnemyTypes.NumberOfTypes;
                _releasedEnemies = new List<Enemy>[_typesCount];
                _enemies = new Queue<Enemy>[_typesCount];
                for (int i = 0; i < _typesCount;i++)
                {
                    _enemies[i] = new Queue<Enemy>();
                    _releasedEnemies[i] = new List<Enemy>();
                }
            }
            
            public bool GetFromPool(out GameObject enemyInstance,Vector3 position, Quaternion rotation)
            {
                Debug.Log("1");
                enemyInstance = null;
                int random = Random.Range(0, _chancesSum);
                int pastChancesSum = 0;
                int enemyTypeInt = 0;
                GameObject prefab = null;
                
                foreach (var enemySpawnData in _enemySpawnDatas)
                {
                    if (random < pastChancesSum + enemySpawnData.SpawnChance && random > pastChancesSum)
                    {
                        enemyTypeInt = (int)enemySpawnData.EnemyType;
                        prefab = enemySpawnData.Prefab;
                        break;
                    }
                    pastChancesSum += enemySpawnData.SpawnChance;
                }

                if (prefab == null)
                    return false;
                Debug.Log("2");
                if (_enemies[enemyTypeInt].Count == 0)
                {
                    CreateEnemy(prefab,position, rotation);
                }
                
                Enemy enemy = null;
                while (enemy == null)
                {
                    enemy = _enemies[enemyTypeInt].Dequeue();
                    if (_enemies.Length == 0)
                        CreateEnemy(prefab, position, rotation);
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
            
            public void OnLevelChange(LevelData levelData)
            {
                _enemySpawnDatas = levelData.EnemySpawnData;
                foreach (var enemySpawnData in levelData.EnemySpawnData)
                {
                    _chancesSum += enemySpawnData.SpawnChance;
                }
            }
            
            private void CreateEnemy(GameObject prefab, Vector3 position, Quaternion rotation)
            {
                GameObject enemyInstance = Object.Instantiate(prefab, position, rotation);
                if (enemyInstance.TryGetComponent(out Enemy enemy))
                {
                    Debug.Log("3");
                    enemy.Construct(_baseHealth);
                    enemy.OnLifeEnd += () => ReturnToPool(enemy);
                    enemy.OnEnemyDestroy += () => _count--;
                    ReturnToPool(enemy);
                    _count++;
                    OnGoAttackBase += enemy.GoAttackBase;
                    OnReturnToSpawnPoint += enemy.GoBackToSpawn;
                }
            }

            public void GoAttackBase()
            {
                OnGoAttackBase?.Invoke();
            }
            public void ReturnToSpawnPoint()
            {
                OnReturnToSpawnPoint?.Invoke();
            }
    }
}
