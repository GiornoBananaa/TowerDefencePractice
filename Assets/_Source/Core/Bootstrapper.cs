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
        [SerializeField] private UnitInspector _unitInspector;
        [SerializeField] private Player _player;
        private PlayerInvoker _playerInvoker;
        private PlayerMovement _playerMovement;
        private PlayerUnitSpawner _playerUnitSpawner;
        
        private void Awake()
        {
            _playerMovement = new PlayerMovement(_player.NavMeshAgent);
            _playerUnitSpawner = new PlayerUnitSpawner();
            _playerInvoker = new PlayerInvoker(_playerMovement, _playerUnitSpawner, _unitInspector);
            _inputListener.Construct(_playerInvoker);
        }
    }
}
