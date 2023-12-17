using UnityEngine;

namespace UISystem
{
    public class BillboardFX : MonoBehaviour
    {
        private Transform _cameraTransform;
        
        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }
        
        private void Update()
        {
            transform.rotation = _cameraTransform.rotation;
        }
    }
}
