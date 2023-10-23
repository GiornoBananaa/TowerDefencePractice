using InputSystem;
using PlayerSystem;
using UnitSystem.TowerSystem;
using UISystem;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private InputListener inputListener;
        [SerializeField] private UnitInspector unitInspector;
        private PlayerInvoker _playerInvoker;
        private PlayerUnitSpawner _playerUnitSpawner;
        
        private void Awake()
        {
            _playerUnitSpawner = new PlayerUnitSpawner();
            _playerInvoker = new PlayerInvoker(_playerUnitSpawner, unitInspector);
            inputListener.Construct(_playerInvoker);
        }
    }
}
