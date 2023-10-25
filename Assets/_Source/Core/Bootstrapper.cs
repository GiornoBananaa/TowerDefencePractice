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
        [SerializeField] private UnitInspector _unitInspector;
        [SerializeField] private BaseHealth _baseHealth;
        [SerializeField] private Player _player;
        [SerializeField] private GameObject[] _enemyPrefabs;
        private PlayerInvoker _playerInvoker;
        private PlayerMovement _playerMovement;
        private TowerSpawner _towerSpawner;
        private EnemyPool _enemyPool;
        private Game _game;
        
        //TODO give data through scriptable object
        private void Awake()
        {
            _game = new Game();
             _baseHealth.Construct(_game);
            _playerMovement = new PlayerMovement(_player.NavMeshAgent);
            _towerSpawner = new TowerSpawner();
            _playerInvoker = new PlayerInvoker(_playerMovement, _towerSpawner, _unitInspector);
            _inputListener.Construct(_playerInvoker);
            _enemyPool = new EnemyPool(new [] { EnemyTypes.Cube,EnemyTypes.Circle}, _enemyPrefabs,_baseHealth);
            _enemySpawner.Construct(_enemyPool);
        }
    }
}
