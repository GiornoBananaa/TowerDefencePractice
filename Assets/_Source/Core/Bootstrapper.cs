using BaseSystem;
using EnemySystem;
using InputSystem;
using PlayerSystem;
using TowerSystem;
using UISystem;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private HUDUpdater _hudUpdater;
        [SerializeField] private UnitInspector _unitInspector;
        [SerializeField] private BaseHealth _baseHealth;
        [SerializeField] private Player _player;
        [SerializeField] private PlayerItemCollector _playerItemCollector;
        [SerializeField] private TowerPlacer _towerPlacer;
        [SerializeField] private GameObject[] _enemyPrefabs;
        [SerializeField] private GameObject[] _towerPrefabs;
        private PlayerInvoker _playerInvoker;
        private PlayerMovement _playerMovement;
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
            _playerMovement = new PlayerMovement(_player.NavMeshAgent);
            _towerSpawner = new TowerSpawner(_towerPrefabs);
            _playerInventory = new PlayerInventory();
            _playerInventory.OnCoinsCountChange += _hudUpdater.CoinsCountUpdate;
            _playerItemCollector.Construct(_playerInventory);
            _playerInvoker = new PlayerInvoker(_playerMovement, _towerSpawner, _unitInspector, _playerInventory,_towerPlacer);
            _inputListener.Construct(_playerInvoker);
            _enemyPool = new EnemyPool(new [] { EnemyTypes.Cube,EnemyTypes.Circle}, _enemyPrefabs,_baseHealth);
            _enemySpawner.Construct(_enemyPool);
        }
    }
}
