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
        [SerializeField] private Player _player;
        [SerializeField] private GameObject[] _enemyPrefabs;
        private PlayerInvoker _playerInvoker;
        private PlayerMovement _playerMovement;
        private PlayerUnitSpawner _playerUnitSpawner;
        private EnemyPool _enemyPool;
        
        //TODO give data through scriptable object
        private void Awake()
        {
            _playerMovement = new PlayerMovement(_player.NavMeshAgent);
            _playerUnitSpawner = new PlayerUnitSpawner();
            _playerInvoker = new PlayerInvoker(_playerMovement, _playerUnitSpawner, _unitInspector);
            _inputListener.Construct(_playerInvoker);
            _enemyPool = new EnemyPool(new [] { EnemyTypes.Cube}, _enemyPrefabs);
            _enemySpawner.Construct(_enemyPool);
        }
    }
}
