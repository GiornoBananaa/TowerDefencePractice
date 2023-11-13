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

        private bool _isOccupied;
        private bool _isSelected;

        public bool IsOccupied => _isOccupied;
        
        private void Awake()
        {
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
        
        public Transform GetTowerPlaceAndDisable()
        {
            _collider.enabled = false;
            _projector.enabled = false;
            _isOccupied = true;
            return _spawnPoint;
        }
    }
}
