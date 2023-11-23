using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System;
using System.Collections.Generic;
using TowerSystem;
using UISystem;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerSystem
{
    [Serializable]
    public class Branch
    {
        public GameObject branch;
        public TowerCell[] towerCells;
        public ObjectOutlineControl outline;
    }
    
    public class ObjectsSelector : MonoBehaviour
    {
        private Branch _selectedBranch;
        private TowerCell _selectedCell;
        
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private ObjectOutlineControl _treeOutline;
        [SerializeField] private Branch[] _branches;

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
                    _selectedBranch.outline.EnableOutline(true);
                    foreach (var towerCell in _selectedBranch.towerCells)
                    {
                        towerCell.gameObject.SetActive(false);
                    }
                }
                
                _selectedBranch = value;
                
                _selectedCell = null;
                
                if (_selectedBranch != null)
                {
                    _treeOutline.EnableOutline(true);
                    _selectedBranch.outline.EnableOutline(false);
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
                {
                    _selectedCell.SelectCell(false);
                }
                _selectedCell = value;
                if(_selectedCell != null)
                {
                    _selectedCell.SelectCell(true);
                }
            }
        }

        public void SelectTree(RaycastHit hitInfo)
        {
            _treeOutline.EnableOutline(false);
            SelectedBranch = null;
            SelectedCell = null;
            _cameraController.FocusOnObject(hitInfo.collider.transform, false);
            foreach (var branch in _branches)
            {
                branch.outline.EnableOutline(true);
            }
        }
        
        public void UnselectAll()
        {
            _treeOutline.EnableOutline(true);
            SelectedBranch = null;
            SelectedCell = null;
            _cameraController.FocusOnObject(null, false);
            foreach (var branch in _branches)
            {
                branch.outline.EnableOutline(false);
            }
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
