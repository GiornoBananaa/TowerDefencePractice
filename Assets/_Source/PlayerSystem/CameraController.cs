using Cinemachine;
using UnityEngine;

namespace PlayerSystem
{
    public enum CameraState
    {
        FreeCamera = 0,
        FocusOnSelected = 1,
        FollowPlayer = 2,
        FocusOnTree = 3,
    }
    
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private CameraState _cameraState;
        [SerializeField] private Transform _treeTransform;
        [SerializeField] private Player _player;
        [Space(2)]
        [Header("Moving")]
        [SerializeField] private float _moveSpeed = 5;
        [SerializeField] private float _borderXmax = 30;
        [SerializeField] private float _borderXmin = -30;
        [SerializeField] private float _borderZmax = 30;
        [SerializeField] private float _borderZmin = -30;
        [Space(2)]
        [Header("Rotation")]
        [SerializeField] private float _sensitivity = 3;
        [Space(2)]
        [Header("Zoom")]
        [SerializeField] private float _zoomSensitivity = 2f;
        [SerializeField] private float _smoothnes = 0.2f;
        [SerializeField] private float _zoomMax = 50;
        [SerializeField] private float _zoomMin = 5;

        private Cinemachine3rdPersonFollow _virtualCameraFollow;
        private Transform _target;
        private Transform _selectedObject;
        private float _cameraDistance;
        private float _x;
        private float _y;
        private Vector3 _offset;

        private void Start()
        {
            _virtualCameraFollow = _virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            _target = _virtualCamera.Follow;
            _cameraDistance = _virtualCamera.m_Lens.OrthographicSize;
            _selectedObject = _treeTransform;
            
            SetCameraState(_cameraState);
            
            _offset = new Vector3(_offset.x, _offset.y, -Mathf.Abs(_zoomMax) / 2);
            transform.position = _target.position + _offset;

            _y = -_target.localEulerAngles.x;
            _x = _target.localEulerAngles.y;
        }

        private void LateUpdate()
        {
            if(_cameraState == CameraState.FocusOnSelected)
                FocusOnSelected();
            else if(_cameraState == CameraState.FollowPlayer)
                Follow();
            else if(_cameraState == CameraState.FocusOnTree)
                FocusOnTree();

            RotateCamera();
            ZoomInCamera();
        }

        public void MoveCamera(Vector3 direction)
        {
            if (_cameraState != CameraState.FreeCamera) return;
            direction = _target.TransformVector(direction);
            direction = new Vector3(direction.x, 0, direction.z).normalized;
            _target.localPosition += direction * _moveSpeed;

            _target.position = new Vector3(_target.position.x > _borderXmax ? _borderXmax : (_target.position.x < _borderXmin ? _borderXmin : _target.position.x),
                _target.position.y, 
                _target.position.z > _borderZmax ? _borderZmax : (_target.position.z < _borderZmin ? _borderZmin : _target.position.z));
        }

        private void Follow()
        {
            _target.position = _player.transform.position;
        }
        
        private void FocusOnSelected()
        {
            _target.position = _selectedObject.position;
        }
        
        private void FocusOnTree()
        {
            _target.position = _treeTransform.position;
        }
        private void RotateCamera()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                _target.Rotate(0, _sensitivity*Time.deltaTime, 0, Space.World);
            }
            if (Input.GetKey(KeyCode.E))
            {
                _target.Rotate(0, -_sensitivity*Time.deltaTime, 0, Space.World);
            }
        }

        private void ZoomInCamera()
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && _cameraDistance < _zoomMax)
                _cameraDistance += _zoomSensitivity;
            else if (Input.GetAxis("Mouse ScrollWheel") > 0 && _cameraDistance > _zoomMin)
                _cameraDistance -= _zoomSensitivity;
            
            _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_virtualCamera.m_Lens.OrthographicSize, _cameraDistance, _smoothnes * Time.deltaTime);
        }
        
        public void SetSelectedObject(Transform selected)
        {
            _selectedObject = selected;
            SetCameraState(CameraState.FocusOnSelected);
        }
        
        public void SetCameraState(CameraState camerasTate)
        {
            _cameraState = camerasTate;
            if (_cameraState == CameraState.FollowPlayer)
            {
                _virtualCameraFollow.Damping = new Vector3(5, 5, 5);
            }
            else if (_cameraState == CameraState.FocusOnTree)
            {
                _virtualCameraFollow.Damping = new Vector3(0, 0, 0);
            }
            else
            {
                _virtualCameraFollow.Damping = new Vector3(0, 0, 0);
            }
        }
    }
}
