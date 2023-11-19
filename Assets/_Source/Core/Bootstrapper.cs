using BaseSystem;
using EnemySystem;
using InputSystem;
using PlayerSystem;
using TowerSystem;
using UISystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
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
        [SerializeField] private ObjectsSelector _objectSelector;
        [SerializeField] private CameraController _cameraController;
        private PlayerInvoker _playerInvoker;
        private PlayerInventory _playerInventory;
        private TowerSpawner _towerSpawner;
        private EnemyPool _enemyPool;
        private Game _game;
        
        //TODO give data through scriptable object
        private void Awake()
        {
            _game = new Game();
            _baseHealth.OnBaseDestroy += _game.Lose;
            _baseHealth.OnBaseHealthChange += _hudUpdater.BaseHealthUpdate;
            _towerSpawner = new TowerSpawner(_towerPrefabs);
            _playerInventory = new PlayerInventory();
            _playerInventory.OnCoinsCountChange += _hudUpdater.CoinsCountUpdate;
            _playerInventory.AddCoins(0);
            _playerItemCollector.Construct(_playerInventory);
            _playerInvoker = new PlayerInvoker(_towerSpawner, _unitInspector, _playerInventory,_objectSelector,_cameraController);
            _inputListener.Construct(_playerInvoker);
            _towerOptionsUI.Construct(_playerInvoker);
            _enemyPool = new EnemyPool(new [] { EnemyTypes.Cube,EnemyTypes.Circle}, _enemyPrefabs,_baseHealth);
            _enemySpawner.Construct(_enemyPool);
            _dayAndNightCycle.OnNightStarted += _enemySpawner.StopSpawning;
            _dayAndNightCycle.OnDayStarted += _enemySpawner.StartSpawning;
            _dayAndNightCycle.OnDayStarted += () => _enemyPool.OnGoAttackBase?.Invoke();
            _dayAndNightCycle.OnNightStarted += () => _enemyPool.OnReturnToSpawnPoint?.Invoke();
        }
    }
}
