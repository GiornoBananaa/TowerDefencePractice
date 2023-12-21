using System;
using System.Collections.Generic;
using System.Linq;
using LevelSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _spawnRadius;
        private List<float> _spawnsActivationTime;
        private float _spawnCooldown;
        private float _spawnTimeElapsed;
        private float _activationTimeElapsed;
        private float _activationTime;
        private EnemyPool _enemyPool;
        private List<Transform> _deactivatedSpawnPoints;
        private List<Transform> _activatedSpawnPoints;

        public int KilledEnemies => _enemyPool.KilledEnemies;

        private void Awake()
        {
            _deactivatedSpawnPoints = _spawnPoints.ToList();
            _activatedSpawnPoints = new List<Transform>();
        }
        
        private void Start()
        {
            for (int i = 0; i < _deactivatedSpawnPoints.Count; i ++)
            {
                if (_spawnsActivationTime.Count <= i)
                {
                    _activatedSpawnPoints.Add(_deactivatedSpawnPoints[i]);
                    _deactivatedSpawnPoints.Remove(_deactivatedSpawnPoints[i]);
                    i--;
                }
            }
        }
        
        public void Construct(EnemyPool enemyPool)
        {
            _enemyPool = enemyPool;
        }
        
        private void Update()
        {
            CheckCooldown();
            CheckSpawnsActivationTime();
        }
        
        public void OnWaveChange(LevelData levelData)
        {
            _spawnCooldown = levelData.EnemiesSpawnCooldown;
            if (levelData.SpawnsActivationTime != null)
                _spawnsActivationTime = levelData.SpawnsActivationTime.ToList();
            else
                _spawnsActivationTime = new List<float>();
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
        
        private void CheckSpawnsActivationTime()
        {
            _activationTimeElapsed += Time.deltaTime;
            for (int i = 0; i < _spawnsActivationTime.Count; i ++)
            {
                if (_activationTimeElapsed >= _spawnsActivationTime[i] && _deactivatedSpawnPoints.Count > i)
                {
                    _activatedSpawnPoints.Add(_deactivatedSpawnPoints[i]);
                    _deactivatedSpawnPoints.Remove(_deactivatedSpawnPoints[i]);
                    _spawnsActivationTime.RemoveAt(i);
                    i--;
                }
            }
        }
        
        private void CheckCooldown()
        {
            _spawnTimeElapsed += Time.deltaTime;
            if (_spawnTimeElapsed >= _spawnCooldown)
            {
                _spawnTimeElapsed = 0;
                foreach (var spawnPoint in _activatedSpawnPoints)
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
            Transform spawnpoint = _spawnPoints[0];
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
