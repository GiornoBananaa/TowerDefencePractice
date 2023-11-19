using System;
using TowerSystem;
using UISystem;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerInvoker
    {
        private readonly CameraController _cameraController;
        private readonly TowerSpawner _towerSpawner;
        private readonly UnitInspector _unitInspector;
        private readonly PlayerInventory _playerInventory;
        private readonly ObjectsSelector _objectSelector;

        public Action OnTowerCellSelect;
        
        public PlayerInvoker(TowerSpawner towerSpawner,UnitInspector unitInspector,
            PlayerInventory playerInventory,ObjectsSelector objectSelector,CameraController cameraController)
        {
            _playerInventory = playerInventory;
            _towerSpawner = towerSpawner;
            _unitInspector = unitInspector;
            _objectSelector = objectSelector;
            _cameraController = cameraController;
        }
        
        public bool SpawnUnit(TowerType towerType)
        {
            if(_objectSelector.SelectedCell != null && !_objectSelector.SelectedCell.IsOccupied && SpendCoins(5))
            {
                Transform towerTransform = _objectSelector.SelectedCell.GetTowerPlaceAndDisable(out Vector3 attackRangePoint);
                _towerSpawner.SpawnUnit(towerType, towerTransform.position, towerTransform.localRotation, attackRangePoint);
                return true;
            }

            return false;
        }
        
        public void MoveCamera(Vector3 direction)
        {
            _cameraController.MoveCamera(direction);
        }
        
        public void InspectUnit(RaycastHit hit)
        {
            _unitInspector.InspectUnit(hit);
        }
        
        public void SelectBranch(RaycastHit hitInfo)
        {
            _objectSelector.SelectBranch(hitInfo);
        }
        
        public void SelectCell(RaycastHit hitInfo)
        {
            _objectSelector.SelectCell(hitInfo);
            OnTowerCellSelect.Invoke();
        }

        public void UnselectAll(RaycastHit hitInfo)
        {
            _objectSelector.UnselectAll();
            _cameraController.FocusOnObject(null,true);
        }
        
        public void SelectTree(RaycastHit hitInfo)
        {
            _objectSelector.SelectTree(hitInfo);
        }
        
        public bool SpendCoins(int count)
        {
            return _playerInventory.SpendCoins(count);
        }
    }
}
