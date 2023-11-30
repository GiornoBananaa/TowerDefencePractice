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
        [SerializeField] private LevelTimer _levelTimer;
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private HUDUpdater _hudUpdater;
        [SerializeField] private UnitInspector _unitInspector;
        [SerializeField] private TowerOptionsUI _towerOptionsUI;
        [SerializeField] private BaseHealth _baseHealth;
        [SerializeField] private Player _player;
        [SerializeField] private PlayerItemCollector _playerItemCollector;
        [SerializeField] private DayAndNightCycle _dayAndNightCycle;
        [SerializeField] private GameObject[] _enemyPrefabs;
        [SerializeField] private GameObject[] _towerPrefabs;
        [SerializeField] private TowerDataSO _squirrelData;
        [SerializeField] private TowerDataSO _squirrel2Data;
        [SerializeField] private ObjectsSelector _objectSelector;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private BuildingModeButton _buildingModeButton;
        private PlayerInvoker _playerInvoker;
        private PlayerInventory _playerInventory;
        private TowerSpawner _towerSpawner;
        private EnemyPool _enemyPool;
        private Game _game;
        private LevelSetter _levelSetter;
        
        //TODO give data through scriptable object
        private void Awake()
        {
            _levelSetter = new LevelSetter(_levelsData.LevelsData);
            _game = new Game(_levelSetter);
            _baseHealth.OnBaseDestroy += _game.Lose;
            _baseHealth.OnBaseHealthChange += _hudUpdater.BaseHealthUpdate;
            _towerSpawner = new TowerSpawner(_towerPrefabs);
            _playerInventory = new PlayerInventory();
            _playerInventory.OnCoinsCountChange += _hudUpdater.CoinsCountUpdate;
            _playerInventory.AddCoins(0);
            _playerItemCollector.Construct(_playerInventory);
            _playerInvoker = new PlayerInvoker(_towerSpawner, _unitInspector, _playerInventory, _objectSelector, _cameraController, new Dictionary<TowerType, TowerDataSO>() { { TowerType.Basic, _squirrelData }, { TowerType.Basic2, _squirrel2Data } });
            _buildingModeButton.OnBuildModeEnable += _objectSelector.SelectTree;
            _buildingModeButton.OnBuildModeDisable += _objectSelector.UnselectAll;
            _objectSelector.OnBuildModeEnable += _buildingModeButton.EnableBuildView;
            _objectSelector.OnBuildModeDisable += _buildingModeButton.DisableBuildView;
            _inputListener.Construct(_playerInvoker);
            _towerOptionsUI.Construct(_playerInvoker);
            _enemyPool = new EnemyPool(_baseHealth);
            _levelSetter.OnLevelChange += _enemyPool.OnLevelChange;
            _enemySpawner.Construct(_enemyPool);
            _levelSetter.OnLevelChange += _enemySpawner.OnLevelChange;
            _levelSetter.OnLevelChange += _levelTimer.OnLevelChange;
            _levelSetter.NextLevel();
            _levelTimer.OnAttackEnd += _enemySpawner.StopSpawning;
            _levelTimer.OnAttackEnd += _levelSetter.NextLevel;
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
            _levelSetter.OnLevelChange -= _enemyPool.OnLevelChange;
            _levelSetter.OnLevelChange -= _enemySpawner.OnLevelChange;
            _levelTimer.OnAttackEnd -= _enemySpawner.StopSpawning;
            _levelTimer.OnAttackEnd -= _levelSetter.NextLevel;
            _levelTimer.OnAttackEnd -= _enemyPool.ReturnToSpawnPoint;
            _levelTimer.OnAttackStart -= _enemySpawner.StartSpawning;
            _levelTimer.OnAttackStart -= _enemyPool.GoAttackBase;
        }
    }
}
