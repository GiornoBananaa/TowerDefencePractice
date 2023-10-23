using PlayerSystem;
using UnityEngine;

namespace InputSystem
{
    public class InputListener : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask towerLayer;
        
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
                ReadBuildTower(hit);
                ReadInspectTower(hit);
            }
        }
        
        private void ReadBuildTower(RaycastHit hit)
        {
            if (groundLayer != (groundLayer | (1 << hit.collider.gameObject.layer)))
                return;

            _playerInvoker.SpawnUnit(hit);
        }
        
        private void ReadInspectTower(RaycastHit hit)
        {
            if (groundLayer != (towerLayer | (1 << hit.collider.gameObject.layer)))
                return;

            _playerInvoker.InspectUnit(hit);
        }
    }
}
