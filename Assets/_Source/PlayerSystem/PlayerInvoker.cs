using TowerSystem;
using UISystem;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerInvoker
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerUnitSpawner _playerUnitSpawner;
        private readonly UnitInspector _unitInspector;

        public PlayerInvoker(PlayerMovement playerMovement,PlayerUnitSpawner playerUnitSpawner,UnitInspector unitInspector)
        {
            _playerMovement = playerMovement;
            _playerUnitSpawner = playerUnitSpawner;
            _unitInspector = unitInspector;
        }
        
        public void SpawnUnit(RaycastHit hit)
        {
            _playerUnitSpawner.SpawnUnit(hit);
        }
        
        public void InspectUnit(RaycastHit hit)
        {
            _unitInspector.InspectUnit(hit);
        }
        
        public void SetNewPlayerPosition(RaycastHit hitInfo)
        {
            _playerMovement.SetNewPosition(hitInfo);
        }
    }
}
