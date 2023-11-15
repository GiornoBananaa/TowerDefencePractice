using System;
using TowerSystem;
using UnityEngine;

namespace PlayerSystem
{
    [Serializable]
    public class Branch
    {
        public GameObject branch;
        public TowerCell[] towerCells;
    }
    
    public class ObjectsSelector : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private Branch[] _branches;
        
        private Branch _selectedBranch;
        private TowerCell _selectedCell;

        public Branch SelectedBranch
        {
            get
            {
                return _selectedBranch;
            }
            set
            {
                if(_selectedBranch == value) return;
                
                if (_selectedBranch != null)
                {
                    foreach (var towerCell in _selectedBranch.towerCells)
                    {
                        towerCell.gameObject.SetActive(false);
                    }
                }
                
                _selectedBranch = value;

                _selectedCell = null;
                
                if (_selectedBranch != null)
                {
                    
                    foreach (var towerCell in _selectedBranch.towerCells)
                    {
                        towerCell.gameObject.SetActive(true);
                    }
                }
            }
        }

        public TowerCell SelectedCell
        {
            get
            {
                return _selectedCell;
            }
            
            set
            {
                if(_selectedCell != null)
                    _selectedCell.SelectCell(false);
                _selectedCell = value;
                if(_selectedCell != null)
                    _selectedCell.SelectCell(true);
            }
        }

        public void SelectTree(RaycastHit hitInfo)
        {
            SelectedBranch = null;
            SelectedCell = null;
            _cameraController.FocusOnObject(hitInfo.collider.transform, false);
        }
        
        public void UnselectAll()
        {
            SelectedBranch = null;
            SelectedCell = null;
            _cameraController.FocusOnObject(null, false);
        }
        
        public void SelectBranch(RaycastHit hitInfo)
        {
            GameObject selected = hitInfo.collider.gameObject;
            
            foreach (var branch in _branches)
            {
                if (branch.branch.gameObject == selected)
                {
                    SelectedBranch = branch;
                    _cameraController.FocusOnObject(hitInfo.collider.transform, false);
                    return;
                }
            }
        }
        
        public void SelectCell(RaycastHit hitInfo)
        {
            GameObject selected = hitInfo.collider.gameObject;
            foreach (var towerCell in _selectedBranch.towerCells)
            {
                if (towerCell.gameObject == selected)
                {
                    SelectedCell = towerCell;
                    return;
                }
            }
        }
    }
}
