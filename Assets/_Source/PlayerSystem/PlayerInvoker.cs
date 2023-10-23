using UISystem;
using UnitSystem.TowerSystem;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerInvoker
    {
        private readonly PlayerUnitSpawner _playerUnitSpawner;
        private readonly UnitInspector _unitInspector;

        public PlayerInvoker(PlayerUnitSpawner playerUnitSpawner,UnitInspector unitInspector)
        {
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
    }
}
