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
            public int KilledEnemies;
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
                
                if (_enemies[enemyTypeInt].Count == 0)
                {
                    CreateEnemy(prefab,position, rotation);
                }
                
                Enemy enemy = null;
                while (enemy == null)
                {
                    if (_enemies.Length != 0)
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
            
            public void OnWaveChange(LevelData levelData)
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
                    enemy.Construct(_baseHealth);
                    enemy.OnReturnToPool += () => { ReturnToPool(enemy);KilledEnemies++; };
                    ReturnToPool(enemy);
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
