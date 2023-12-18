using PlayerSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace InputSystem
{
    public class InputListener : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _branchLayer;
        [SerializeField] private LayerMask _towerCellLayer;
        [SerializeField] private LayerMask _towerLayer;
        [SerializeField] private LayerMask _treeLayer;
        
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
            ReadMoveCamera();
        }
        //TODO: Fix tower click
        private void ClickRaycast()
        {
            if(!Input.GetMouseButtonDown(0)) return;
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if(!Physics.Raycast(ray, out RaycastHit _raycastHit) || EventSystem.current.IsPointerOverGameObject()) return;
            int hitLayerBite = (1 << _raycastHit.collider.gameObject.layer);
            ReadSelectTowerCell(_raycastHit,hitLayerBite);
            ReadSelectBranch(_raycastHit,hitLayerBite);
            ReadInspectTower(_raycastHit,hitLayerBite);
            //ReadSelectFreeView(_raycastHit,hitLayerBite);
            ReadSelectTree(hitLayerBite);
        }

        private void ReadMoveCamera()
        {
            float x = 0, z = 0;
        
            if (Input.GetKey(KeyCode.W))
            {
                z += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                z -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                x += 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                x -= 1;
            }

            Vector3 direction = new Vector3(x, 0 ,z);
            
            _playerInvoker.MoveCamera(direction);
        }
        private void ReadInspectTower(RaycastHit hit,int hitLayerBite)
        {
            if( _towerLayer == (_towerLayer | hitLayerBite))
                _playerInvoker.InspectUnit(hit);
        }
        
        private void ReadSelectBranch(RaycastHit hit,int hitLayerBite)
        {
            if( _branchLayer == (_branchLayer | hitLayerBite))
                _playerInvoker.SelectBranch(hit);
        }
        
        private void ReadSelectFreeView(RaycastHit hit,int hitLayerBite)
        {
            if( _groundLayer == (_groundLayer | hitLayerBite))
                _playerInvoker.UnselectAll(hit);
        }
        
        private void ReadSelectTowerCell(RaycastHit hit,int hitLayerBite)
        {
            if( _towerCellLayer == (_towerCellLayer | hitLayerBite))
                _playerInvoker.SelectCell(hit);
        }
        
        private void ReadSelectTree(int hitLayerBite)
        {
            if( _treeLayer == (_treeLayer | hitLayerBite))
                _playerInvoker.SelectTree();
        }
    }
}
