using UnityEngine;

namespace PlayerSystem
{
    public class TowerPlacer : MonoBehaviour
    {
        [SerializeField] private float _gridSize;
        [SerializeField] private float _gridHeight;
        [SerializeField] private Transform _player;
        [SerializeField] private Projector _projector;
        [SerializeField] private Vector2 _baseGridPositions;
        [SerializeField] private Vector2 _roadsGridPositions;
        
        private Vector2 _gridPosition;
        private bool _isOnGrid;

        private bool IsOnGrid
        {
            get => _isOnGrid;
            set
            {
                _isOnGrid = value;
                _projector.enabled = value;
            }
        }

        private void Awake()
        {
            IsOnGrid = false;
            _projector.orthographicSize = _gridSize/2;
        }

        private void Update()
        {
            CheckGridPosition();
        }

        private void CheckGridPosition()
        {
            Vector3 position = _player.position;
            _gridPosition = new Vector2(
                Mathf.Round(position.x / _gridSize),
                Mathf.Round(position.z / _gridSize));
            if(_gridPosition.x < _baseGridPositions.x && _gridPosition.x > -_baseGridPositions.x 
                    && _gridPosition.y < _baseGridPositions.y && _gridPosition.y > -_baseGridPositions.y
                ||_gridPosition.x < _roadsGridPositions.x && _gridPosition.x > -_roadsGridPositions.x 
                    && _gridPosition.y < _roadsGridPositions.y && _gridPosition.y > -_roadsGridPositions.y
                ||_gridPosition.x < _roadsGridPositions.y && _gridPosition.x > -_roadsGridPositions.y 
                    && _gridPosition.y < _roadsGridPositions.x && _gridPosition.y > -_roadsGridPositions.x)
            {
                IsOnGrid = false;
                return;
            }
            
            transform.position = new Vector3(
                _gridPosition.x * _gridSize,
                _gridHeight,
                _gridPosition.y * _gridSize);
            IsOnGrid = true;
        }
        
        public bool TryGetPlacerPosition(out Vector3 position)
        {
            if (IsOnGrid)
            {
                position = transform.position;
                return true;
            }
            
            position = Vector3.zero;
            return false;
        }
    }
}
