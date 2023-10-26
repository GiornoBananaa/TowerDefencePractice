using System.Collections.Generic;
using BaseSystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyPool
    {
            private Queue<GameObject>[] _enemies;
            private List<GameObject>[] _releasedEnemies;
            private GameObject[] _enemiesPrefab;
            private int _count;
            private int _typesCount;
            private BaseHealth _baseHealth;
            private EnemyTypes[] _availableTypes;


            public EnemyPool(EnemyTypes[] availableTypes,GameObject[] enemiesPrefab,BaseHealth baseHealth)
            {
                _baseHealth = baseHealth;
                _enemiesPrefab = enemiesPrefab;
                _availableTypes = availableTypes;
                _typesCount = (int)EnemyTypes.NumberOfTypes;
                _releasedEnemies = new List<GameObject>[availableTypes.Length];
                _enemies = new Queue<GameObject>[availableTypes.Length];
                for (int i = 0; i < availableTypes.Length;i++)
                {
                    _enemies[i] = new Queue<GameObject>();
                    _releasedEnemies[i] = new List<GameObject>();
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

                enemyInstance = null;
                while (enemyInstance == null)
                {
                    enemyInstance = _enemies[enemyTypeInt].Dequeue();
                }

                _releasedEnemies[enemyTypeInt].Add(enemyInstance);
                enemyInstance.transform.position = position;
                enemyInstance.transform.rotation = rotation;
                enemyInstance.SetActive(true);
                
                return true;
            }

            public void ReturnToPool(Enemy enemy)
            {
                int enemyType = (int)enemy.EnemyType;
                _releasedEnemies[enemyType].Remove(enemy.gameObject);
                _enemies[enemyType].Enqueue(enemy.gameObject);
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
                }
            }
    }
}
