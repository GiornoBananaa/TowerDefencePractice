using System;
using System.Collections.Generic;
using BaseSystem;
using EnemySystem;
using InputSystem;
using LevelSystem;
using PlayerSystem;
using TowerSystem;
using UISystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private LevelsDataSO _levelsData;
        [SerializeField] private TowersDataSO _towersDataByLevel;
        [SerializeField] private TowersSpawnDataSO _towersSpawnData;
        [SerializeField] private LevelTimer _levelTimer;
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private HUDUpdater _hudUpdater;
        [FormerlySerializedAs("_unitInspector")] [SerializeField] private TowerInspector _towerInspector;
        [SerializeField] private TowerOptionsUI _towerOptionsUI;
        [SerializeField] private BaseHealth _baseHealth;
        [SerializeField] private PlayerItemCollector _playerItemCollector;
        [SerializeField] private DayAndNightCycle _dayAndNightCycle;
        [SerializeField] private ObjectsSelector _objectSelector;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private BuildingModeButton _buildingModeButton;
        private PlayerInvoker _playerInvoker;
        private PlayerInventory _playerInventory;
        private TowerSpawner _towerSpawner;
        private EnemyPool _enemyPool;
        private Game _game;
        private WaveSetter _waveSetter;
        
        
        private void Awake()
        {
            Dictionary<TowerType, TowerData[]> towersDictionary = new Dictionary<TowerType, TowerData[]>()
            {
                { TowerType.BasicSquirrel, _towersDataByLevel.BasicSquirrelDataByLevels },
                { TowerType.Squirrel2, _towersDataByLevel.Squirrel2DataByLevels },
                { TowerType.BerserkSquirrel, _towersDataByLevel.BerserkSquirrelDataByLevels }
            };
            
            Dictionary<TowerType, GameObject> towersSpawnDataDictionary = new Dictionary<TowerType, GameObject>()
            {
                { TowerType.BasicSquirrel, _towersSpawnData.TowersSpawnData[0].Prefab },
                { TowerType.Squirrel2, _towersSpawnData.TowersSpawnData[1].Prefab },
                { TowerType.BerserkSquirrel, _towersSpawnData.TowersSpawnData[2].Prefab }
            };
            
            _waveSetter = new WaveSetter(_levelsData);
            _game = new Game();
            _baseHealth.OnBaseDestroy += _game.Lose;
            _baseHealth.OnBaseHealthChange += _hudUpdater.BaseHealthUpdate;
            _towerSpawner = new TowerSpawner(towersDictionary,towersSpawnDataDictionary);
            _playerInventory = new PlayerInventory();
            _playerInventory.OnCoinsCountChange += _hudUpdater.CoinsCountUpdate;
            _playerInventory.AddCoins(10);
            _playerItemCollector.Construct(_playerInventory);
            _playerInvoker = new PlayerInvoker(_towerSpawner, _towerInspector, _playerInventory, _objectSelector, _cameraController, towersDictionary);
            _towerInspector.Construct(_playerInvoker);
            _buildingModeButton.OnBuildModeEnable += _objectSelector.SelectTree;
            _buildingModeButton.OnBuildModeDisable += _objectSelector.UnselectAll;
            _objectSelector.OnBuildModeEnable += _buildingModeButton.EnableBuildView;
            _objectSelector.OnBuildModeDisable += _buildingModeButton.DisableBuildView;
            _inputListener.Construct(_playerInvoker);
            _towerOptionsUI.Construct(_playerInvoker,towersDictionary);
            _enemyPool = new EnemyPool(_baseHealth);
            _waveSetter.OnWaveChange += _enemyPool.OnWaveChange;
            _enemySpawner.Construct(_enemyPool);
            _waveSetter.OnWaveChange += _enemySpawner.OnWaveChange;
            _waveSetter.OnWaveChange += _levelTimer.OnWaveChange;
            _waveSetter.SetWave();
            _levelTimer.OnAttackEnd += _enemySpawner.StopSpawning;
            _levelTimer.OnAttackEnd += _waveSetter.NextWave;
            _levelTimer.OnAttackEnd += _enemyPool.ReturnToSpawnPoint;
            _levelTimer.OnAttackStart += _enemySpawner.StartSpawning;
            _levelTimer.OnAttackStart += _enemyPool.GoAttackBase;
            /*   DAY AND NIGHT CYCLE MIGHT BE REMOVED
            _dayAndNightCycle.OnNightStarted += _enemySpawner.StopSpawning;
            _dayAndNightCycle.OnDayStarted += _enemySpawner.StartSpawning;
            _dayAndNightCycle.OnDayStarted += _enemyPool.GoAttackBase;
            _dayAndNightCycle.OnNightStarted += _enemyPool.ReturnToSpawnPoint;
            */
        }

        private void OnDestroy()
        {
            _buildingModeButton.OnBuildModeEnable -= _objectSelector.SelectTree;
            _buildingModeButton.OnBuildModeDisable -= _objectSelector.UnselectAll;
            _objectSelector.OnBuildModeEnable -= _buildingModeButton.EnableBuildView;
            _objectSelector.OnBuildModeDisable -= _buildingModeButton.DisableBuildView;
            _waveSetter.OnWaveChange -= _enemyPool.OnWaveChange;
            _waveSetter.OnWaveChange -= _enemySpawner.OnWaveChange;
            _levelTimer.OnAttackEnd -= _enemySpawner.StopSpawning;
            _levelTimer.OnAttackEnd -= _waveSetter.NextWave;
            _levelTimer.OnAttackEnd -= _enemyPool.ReturnToSpawnPoint;
            _levelTimer.OnAttackStart -= _enemySpawner.StartSpawning;
            _levelTimer.OnAttackStart -= _enemyPool.GoAttackBase;
        }
    }
}
