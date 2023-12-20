using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace PlayerSystem
{
    public enum CameraState
    {
        FreeCamera = 0,
        InTransition = 1,
        Follow = 2,
    }
    
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Transform _treeTransform;
        [Space(2)]
        [Header("Moving")]
        [SerializeField] private float _moveSpeed = 5;
        [SerializeField] private float _deafultHeight;
        [SerializeField] private float _transitionDuration;
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
        private CameraState _cameraState;
        private Vector3 _offset;
        private Transform _focusedTransform;
        private float _cameraDistance;

        private void Start()
        {
            _virtualCameraFollow = _virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            _target = _virtualCamera.Follow;
            _cameraDistance = _virtualCamera.m_Lens.OrthographicSize;

            _cameraState = CameraState.FreeCamera;
            
            _offset = new Vector3(_offset.x, _offset.y, -Mathf.Abs(_zoomMax) / 2);
            transform.position = _target.position + _offset;
        }

        private void LateUpdate()
        {
            RotateCamera();
            ZoomInCamera();
        }
    
        public void MoveCamera(Vector3 direction)
        {
            if (_cameraState != CameraState.FreeCamera || direction == Vector3.zero) return;
            direction = _target.TransformVector(direction);
            direction = new Vector3(direction.x, 0, direction.z).normalized;
            if (Math.Abs(_target.position.y - _deafultHeight) > 0.2f)
                _target.DOMoveY(_deafultHeight,_transitionDuration);
            _target.localPosition += direction * _moveSpeed;
            
            _target.position = new Vector3(_target.position.x > _borderXmax ? _borderXmax : (_target.position.x < _borderXmin ? _borderXmin : _target.position.x),
                _target.position.y, 
                _target.position.z > _borderZmax ? _borderZmax : (_target.position.z < _borderZmin ? _borderZmin : _target.position.z));
        }

        public void ForceStopTransition()
        {
            _target.DOKill();
        }

        public void FocusOnObject(Transform focusTransform, bool follow, float zoom)
        {
            _cameraDistance = zoom;
            if(_focusedTransform == focusTransform && follow) return;
            
            if (follow)
            {
                _target.parent = focusTransform;
            }
            else
            {
                _target.parent = null;
            }
            
            _focusedTransform = focusTransform;
            
            if (focusTransform == null)
            {
                var position = _target.position;
                SetNewPosition(new Vector3(position.x,_deafultHeight,position.z), 
                    _transitionDuration, CameraState.FreeCamera);
            }
            else
            {
                SetNewPosition(focusTransform.position, _transitionDuration, follow? CameraState.Follow:CameraState.FreeCamera);
            }
        }
        
        private void SetNewPosition(Vector3 newPosition, float duration, CameraState onCompleteCameraState)
        {
            _cameraState = CameraState.InTransition;
             _target.DOMove(newPosition, duration).OnComplete(() =>
             {
                 _cameraState = onCompleteCameraState;
             });
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
    }
}
