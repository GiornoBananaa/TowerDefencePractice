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
        private readonly PlayerInventory _playerInventory;
        private readonly TowerPlacer _towerPlacer;

        public PlayerInvoker(PlayerMovement playerMovement,TowerSpawner towerSpawner,UnitInspector unitInspector,PlayerInventory playerInventory,TowerPlacer towerPlacer)
        {
            _towerPlacer = towerPlacer;
            _playerInventory = playerInventory;
            _playerMovement = playerMovement;
            _towerSpawner = towerSpawner;
            _unitInspector = unitInspector;
        }
        
        public void SpawnUnit()
        {
            if(SpendCoins(5) && _towerPlacer.TryGetPlacerPosition(out Vector3 position))
                _towerSpawner.SpawnUnit(TowerType.Basic,position);
        }
        
        public void InspectUnit(RaycastHit hit)
        {
            _unitInspector.InspectUnit(hit);
        }
        
        public void SetNewPlayerPosition(RaycastHit hitInfo)
        {
            _playerMovement.SetNewPosition(hitInfo);
        }
        
        public bool SpendCoins(int count)
        {
            return _playerInventory.SpendCoins(count);
        }
    }
}
