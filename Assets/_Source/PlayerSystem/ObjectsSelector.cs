using System;
using TowerSystem;
using UISystem;
using UnityEditor;
using UnityEngine;

namespace PlayerSystem
{
    [Serializable]
    public class Branch
    {
        public GameObject branch;
        public TowerCell[] towerCells;
        public ObjectOutlineControl outline;

        public Branch(GameObject branch, TowerCell[] towerCells,ObjectOutlineControl outline)
        {
            this.branch = branch;
            this.towerCells = towerCells;
            this.outline = outline;
        }
    }
    
    public class ObjectsSelector : MonoBehaviour
    {
        public Action OnBuildModeEnable;
        public Action OnBuildModeDisable;
        
        private Branch _selectedBranch;
        private TowerCell _selectedCell;
        
        private bool _buildModeSelected;
        
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

        private void Start()
        {
            UnselectAll();
        }

        public void SelectTree()
        {
            _buildModeSelected = true;
            _treeOutline.EnableOutline(false);
            SelectedBranch = null;
            SelectedCell = null;
            _cameraController.FocusOnObject(_treeOutline.transform, true);
            foreach (var branch in _branches)
            {
                branch.outline.EnableOutline(true);
            }
            OnBuildModeEnable?.Invoke();
        }
        
        public void UnselectAll()
        {
            _buildModeSelected = false;
            _treeOutline.EnableOutline(true);
            SelectedBranch = null;
            SelectedCell = null;
            _cameraController.FocusOnObject(null, false);
            foreach (var branch in _branches)
            {
                branch.outline.EnableOutline(false);
            }
            OnBuildModeDisable?.Invoke();
        }
        
        public void SelectBranch(RaycastHit hitInfo)
        {
            if(!_buildModeSelected) return;
            GameObject selected = hitInfo.collider.gameObject;
            
            foreach (var branch in _branches)
            {
                if (branch.branch.gameObject == selected)
                {
                    SelectedBranch = branch;
                    _cameraController.FocusOnObject(hitInfo.collider.transform, true);
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
    
    [CustomEditor(typeof(ObjectsSelector))]
    [CanEditMultipleObjects]
    public class ObjectSelectorSetterSupport : Editor
    {
        private SerializedProperty _branches;

        private void OnEnable()
        {
            _branches = serializedObject.FindProperty("_branches");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();

            if(GUILayout.Button("SetBranches"))
            {
                _branches.arraySize = 0;
                foreach(var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
                {
                    if(gameObj.name == "TowerCells")
                    {
                        TowerCell[] towerCells = gameObj.GetComponentsInChildren<TowerCell>();
                        GameObject branch = gameObj.transform.parent.gameObject;
                        ObjectOutlineControl branchOutline = branch.GetComponent<ObjectOutlineControl>();
                        _branches.arraySize++;
                        _branches.GetArrayElementAtIndex(_branches.arraySize - 1).boxedValue =  new Branch(branch,towerCells,branchOutline);
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}
