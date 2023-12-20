using System.Collections.Generic;
using LevelSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private float _spawnRadius;
        private float _spawnCooldown;
        private float _spawnTimeElapsed;
        private EnemyPool _enemyPool;

        public int KilledEnemies => _enemyPool.KilledEnemies;
        
        public void Construct(EnemyPool enemyPool)
        {
            _enemyPool = enemyPool;
        }
        
        private void Update()
        {
            CheckCooldown();
        }

        public void OnWaveChange(LevelData levelData)
        {
            _spawnCooldown = levelData.EnemiesSpawnCooldown;
        }
        
        public void StartSpawning()
        {
            enabled = true;
            foreach (var spawnPoint in _spawnPoints)
            {
                spawnPoint.gameObject.SetActive(false);
            }
        }
        
        public void StopSpawning()
        {
            enabled = false;
            foreach (var spawnPoint in _spawnPoints)
            {
                spawnPoint.gameObject.SetActive(true);
            }
        }
        
        public void ReturnAllTosSpawn()
        {
            _enemyPool.ReturnToSpawnPoint();
        }
        
        private void CheckCooldown()
        {
            _spawnTimeElapsed += Time.deltaTime;
            if (_spawnTimeElapsed >= _spawnCooldown)
            {
                _spawnTimeElapsed = 0;
                foreach (var spawnPoint in _spawnPoints)
                {
                    SpawnRandomEnemy(spawnPoint);
                }
            }
        }
        
        public GameObject SpawnRandomEnemy(Transform spawnpoint)
        {
            Vector3 position = spawnpoint.position;
            position = new Vector3(
                position.x+Random.Range(-_spawnRadius,_spawnRadius),
                position.y,
                position.z+Random.Range(-_spawnRadius,_spawnRadius));
            
            _enemyPool.GetFromPool(out GameObject enemy, position, spawnpoint.rotation);
            return enemy;
        }
        
        public GameObject SpawnEnemy()
        {
            Transform spawnpoint = _spawnPoints[Random.Range(0,_spawnPoints.Count)];
            Vector3 position = spawnpoint.position;
            position = new Vector3(
                position.x+Random.Range(-_spawnRadius,_spawnRadius),
                position.y,
                position.z+Random.Range(-_spawnRadius,_spawnRadius));
            
            _enemyPool.GetFromPool(out GameObject enemy, position, spawnpoint.rotation);
            return enemy;
        }
    }
}
