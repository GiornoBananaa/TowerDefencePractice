using System;
using System.Collections.Generic;
using TowerSystem;
using UISystem;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerInvoker
    {
        private readonly CameraController _cameraController;
        private readonly TowerSpawner _towerSpawner;
        private readonly TowerInspector _towerInspector;
        private readonly PlayerInventory _playerInventory;
        private readonly ObjectsSelector _objectSelector;
        private readonly Dictionary<TowerType,TowerData[]> _towers;

        public Action<TowerType[]> OnTowerCellSelect;
        public Action<TowerType[]> OnTowerCellDeselect;
        
        public PlayerInvoker(TowerSpawner towerSpawner,TowerInspector towerInspector,
            PlayerInventory playerInventory,ObjectsSelector objectSelector,CameraController cameraController,Dictionary<TowerType,TowerData[]> towers)
        {
            _playerInventory = playerInventory;
            _towerSpawner = towerSpawner;
            _towerInspector = towerInspector;
            _objectSelector = objectSelector;
            _cameraController = cameraController;
            _towers = towers;
        }
        
        public bool SpawnUnit(TowerType towerType)
        {
            if(_objectSelector.SelectedCell != null && !_objectSelector.SelectedCell.IsOccupied && SpendCoins(_towers[towerType][0].Price))
            {
                _objectSelector.SelectedCell.DisableCell();
                _towerSpawner.SpawnUnit(towerType, _objectSelector.SelectedCell);
                return true;
            }
            
            return false;
        }

        public void TurnOffTowerPreview()
        {
            _towerInspector.HideAttackRange();
        }
        
        public void TurnOnTowerPreview(TowerType towerType)
        {
            _towerInspector.ShowAttackRange(_objectSelector.SelectedCell.AttackRangePoint,_towers[towerType][0].AttackRange);
        }
        
        public void MoveCamera(Vector3 direction)
        {
            _cameraController.MoveCamera(direction);
        }
        
        public void InspectUnit(RaycastHit hit)
        {
            Tower tower = hit.transform.GetComponent<Tower>();
            _cameraController.FocusOnObject(tower.transform, false, 15);
            _towerInspector.InspectTower(tower);
        }
        
        public bool UpgradeTower(Tower tower)
        {
            if(SpendCoins(tower.TowerLevelDatas[tower.Level+1].Price))
            {
                tower.UpgradeTower(tower.Level+1);
                return true;
            }
            return false;
        }
        
        public void SelectBranch(RaycastHit hitInfo)
        {
            _objectSelector.SelectBranch(hitInfo);
        }
        
        public void SelectCell(RaycastHit hitInfo)
        {
            _objectSelector.SelectCell(hitInfo);
            OnTowerCellSelect.Invoke(_objectSelector.SelectedCell.AvailableTowerTypes);
        }

        public void UnselectAll(RaycastHit hitInfo)
        {
            _towerInspector.StopInspection();
            _objectSelector.UnselectAll();
        }
        
        public void SelectTree()
        {
            _objectSelector.SelectTree();
        }
        
        public bool SpendCoins(int count)
        {
            return _playerInventory.SpendCoins(count);
        }
    }
}
