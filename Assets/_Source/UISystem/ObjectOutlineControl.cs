using System;
using UnityEngine;

namespace UISystem
{
    [RequireComponent(typeof(Outline))]
    public class ObjectOutlineControl : MonoBehaviour
    {
        [SerializeField] private Outline _outline;
        
        private bool _outlineEnabled;
        private bool _outlineViewed;
        private float _defaultWidth;
        
        private void Awake()
        {
            _defaultWidth = _outline.OutlineWidth;
            _outline.OutlineWidth = 0;
            _outlineViewed = false;
        }
        
        private void OnMouseEnter()
        {
            _outlineViewed = true;
            
            if (!_outlineEnabled) return;
            
            _outline.OutlineWidth = _defaultWidth;
        }
        
        private void OnMouseExit()
        {
            _outlineViewed = false;
            
            if (!_outlineEnabled) return;
            _outline.OutlineWidth = 0;
        }
        
        public void EnableOutline(bool enable)
        {
            _outlineEnabled = enable;
            _outline.OutlineWidth = _outlineViewed && enable ? _defaultWidth : 0;
        }
    }
}
