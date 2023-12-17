using UnityEngine;

namespace UISystem
{
    public class BillboardFX : MonoBehaviour
    {
        private Transform _cameraTransform;

        private Quaternion _defaultRotation;
        
        private void Start()
        {
            _cameraTransform = Camera.main.transform;
            _defaultRotation = _cameraTransform.rotation;
        }
        
        private void Update()
        {
            transform.rotation = _cameraTransform.rotation * _defaultRotation;
        }
    }
}
