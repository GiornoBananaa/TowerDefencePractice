using PlayerSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace InputSystem
{
    public class InputListener : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _towerLayer;
        
        private Camera _camera;
        private PlayerInvoker _playerInvoker;

        public void Construct(PlayerInvoker playerInvoker)
        {
            _playerInvoker = playerInvoker;
            _camera = Camera.main;
        }
        
        private void Update()
        {
            ClickRaycast();
        }

        private void ClickRaycast()
        {
            if(!Input.GetMouseButton(0)) return;
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                ReadMoveToPoint(hit);
            }
        }
        
        private void ReadBuildTower(RaycastHit hit)
        {
            _playerInvoker.SpawnUnit(hit);
        }
        
        private void ReadInspectTower(RaycastHit hit)
        {
            _playerInvoker.InspectUnit(hit);
        }
        
        private void ReadMoveToPoint(RaycastHit hit)
        {
            if (_groundLayer != (_groundLayer | (1 << hit.collider.gameObject.layer)))
                return;

            _playerInvoker.SetNewPlayerPosition(hit);
        }
    }
}
