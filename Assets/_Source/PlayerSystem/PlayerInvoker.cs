using TowerSystem;
using UISystem;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerInvoker
    {
        private readonly PlayerMovement _playerMovement;
        private readonly TowerSpawner _towerSpawner;
        private readonly UnitInspector _unitInspector;

        public PlayerInvoker(PlayerMovement playerMovement,TowerSpawner towerSpawner,UnitInspector unitInspector)
        {
            _playerMovement = playerMovement;
            _towerSpawner = towerSpawner;
            _unitInspector = unitInspector;
        }
        
        public void SpawnUnit(RaycastHit hit)
        {
            _towerSpawner.SpawnUnit(hit);
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
