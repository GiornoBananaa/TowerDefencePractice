using PlayerSystem;
using UnityEngine;

namespace TowerSystem
{
    public class TowerRangePlacer : MonoBehaviour
    {
        
        //SCRIPT IS NOT IN USE (temporarily)
        
        
        [SerializeField] private Projector _maxRangeProjector;
        [SerializeField] private Projector _mouseRangeProjector;
        [SerializeField] private LayerMask _groundLayerMask;

        private bool _isinMavxRange;
        private BaseSquirrel _baseSquirrel;
        private TowerCell _towerCell;
        private PlayerInvoker _playerInvoker;

        private void Awake()
        {
            _isinMavxRange = false;
        }

        public void Construct(PlayerInvoker playerInvoker)
        {
            _playerInvoker = playerInvoker;
            //_playerInvoker.OnTowerRangeSelected += EnableRangeChanging;
        }

        private void Update()
        {
            MoveTowerRange();
            CheckRange();
        }

        public void EnableRangeChanging(BaseSquirrel baseSquirrel, TowerCell towerCell)
        {
            _baseSquirrel = baseSquirrel;
            _towerCell = towerCell;
            enabled = true;
            _maxRangeProjector.enabled = true;
            _mouseRangeProjector.enabled = true;
        }

        public bool TryGetPlacerPosition(out Vector3 position)
        {
            position = Vector3.zero;
            enabled = false;
            _maxRangeProjector.enabled = false;
            _mouseRangeProjector.enabled = false;
            return false;
        }

        private void MoveTowerRange()
        {
            if (_isinMavxRange)
            {
                
            }
        }

        private void CheckRange()
        {
            //if (_mouseRangeProjector.transform.position)
        }

        private void OnDestroy()
        {
            //_playerInvoker.OnTowerRangeSelected -= EnableRangeChanging;
        }
    }
}
