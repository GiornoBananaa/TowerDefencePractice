using System;
using UnityEngine;

namespace TowerSystem
{
    public class TowerCell : MonoBehaviour
    {
        [SerializeField] private Projector _projector;
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Material _deafultMaterial;
        [SerializeField] private Material _highlightingMaterial;
        [SerializeField] private Material _selectionMaterial;
        [SerializeField] private LayerMask _groundLayer;
        
        private bool _isOccupied;
        private bool _isSelected;
        private Vector3 _attackRangePoint;
        
        [field:SerializeField] public TowerType[] AvailableTowerTypes { get; private set; }
        public bool IsOccupied => _isOccupied;
        public Vector3 AttackRangePoint => _attackRangePoint;
        public Transform SpawnPoint => _spawnPoint;
        
        
        private void Awake()
        {
            Ray ray = new Ray(transform.position,Vector3.down); 
            Physics.Raycast(ray, out RaycastHit hit,_groundLayer);
            _attackRangePoint = hit.point;
            _isOccupied = false;
            _isSelected = false;
            gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            _projector.material = _deafultMaterial;
        }
        
        private void OnMouseEnter()
        {
            _projector.material = _highlightingMaterial;
        }
        
        private void OnMouseExit()
        {
            if (_isSelected)
                _projector.material = _selectionMaterial;
            else
                _projector.material = _deafultMaterial;
        }
        
        public void SelectCell(bool isSelected)
        {
            if(!isSelected)
            {
                _isSelected = false;
                _projector.material = _deafultMaterial;
            }
            else
            {
                _isSelected = true;
                _projector.material = _selectionMaterial;
            }
        }
        
        public void DisableCell()
        {
            _collider.enabled = false;
            _projector.enabled = false;
            _isOccupied = true;
        }
        
        public void EnableCell()
        {
            _collider.enabled = true;
            _projector.enabled = true;
            _isOccupied = false;
        }
    }
}
