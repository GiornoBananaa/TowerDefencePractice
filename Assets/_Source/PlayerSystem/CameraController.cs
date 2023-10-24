using Cinemachine;
using UnityEngine;

namespace PlayerSystem
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private bool _followPlayer;
        [SerializeField] private Transform _player;
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
        private float _cameraDistance;
        private float _x;
        private float _y;
        private Vector3 _offset;

        private void Start()
        {
            _virtualCameraFollow = _virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            _target = _virtualCamera.Follow;
            _cameraDistance = _virtualCamera.m_Lens.OrthographicSize;

            SetFollowPlayer(_followPlayer);
            
            _offset = new Vector3(_offset.x, _offset.y, -Mathf.Abs(_zoomMax) / 2);
            transform.position = _target.position + _offset;

            _y = -_target.localEulerAngles.x;
            _x = _target.localEulerAngles.y;
        }

        private void LateUpdate()
        {
            if (!_followPlayer)
                MoveCamera();
            else
                Follow();

            RotateCamera();
            ZoomInCamera();
        }

        private void MoveCamera()
        {
            float x = 0, z = 0;
        
            if (Input.GetKey(KeyCode.W) )
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
            direction = _target.TransformVector(direction);
            direction = new Vector3(direction.x, 0, direction.z).normalized;
            _target.localPosition += direction * _moveSpeed;

            _target.position = new Vector3(_target.position.x > _borderXmax ? _borderXmax : (_target.position.x < _borderXmin ? _borderXmin : _target.position.x),
                _target.position.y, 
                _target.position.z > _borderZmax ? _borderZmax : (_target.position.z < _borderZmin ? _borderZmin : _target.position.z));
        }

        private void Follow()
        {
            _target.position = _player.position;
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

        public void SetFollowPlayer(bool value)
        {
            _followPlayer = value;
            if (_followPlayer)
            {
                _virtualCameraFollow.Damping = new Vector3(5, 5, 5);
            }
            else
            {
                _virtualCameraFollow.Damping = new Vector3(0, 0, 0);
            }
        }
    }
}
