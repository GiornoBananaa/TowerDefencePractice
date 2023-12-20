using System;
using System.Collections;
using UnityEngine;

namespace UISystem
{
    [RequireComponent(typeof(Outline))]
    public class ObjectOutlineControl : MonoBehaviour
    {
        [SerializeField] private Outline _outline;
        [SerializeField] float _defaultWidth;
        [SerializeField] float _highlightedWidth;
        
        private bool _outlineHighlighted;
        private bool _alwaysShow;
        private Color _defaultColor;
        
        
        private void OnEnable()
        {
            _outline.OutlineWidth = _defaultWidth;
            _outlineHighlighted = false;
            _defaultColor = _outline.OutlineColor;
            EnableOutline(false);
        }
        
        private void OnMouseEnter()
        {
            _outlineHighlighted = true;
            
            _outline.OutlineWidth = _highlightedWidth;
        }
        
        private void OnMouseExit()
        {
            _outlineHighlighted = false;
            
            _outline.OutlineWidth = _alwaysShow ? _highlightedWidth :_defaultWidth;
        }
        
        public void EnableOutline(bool enable)
        {
            if(gameObject.activeSelf)
                StartCoroutine(OutlineEnable(enable));
            else
            {
                _outline.OutlineWidth = _outlineHighlighted ? _highlightedWidth : _defaultWidth;
                _outline.enabled = enable;
            }
        }
        
        public void ChangeColor(Color color)
        {
            _outline.OutlineColor = color;
        }
        
        public void ResetColor()
        {
            _outline.OutlineColor = _defaultColor;
        }
        
        public void AlwaysShowMode(bool enable)
        {
            _alwaysShow = enable;
            if (enable)
                _outline.OutlineWidth = _highlightedWidth;
        }
        
        // This coroutine fixes graphic bu*g where outline is showed for one frame when it's enabling
        private IEnumerator OutlineEnable(bool enable)
        {
            _outline.OutlineWidth = _outlineHighlighted ? _highlightedWidth : _defaultWidth;
            yield return new WaitForEndOfFrame();
            _outline.enabled = enable;
        } 
    }
}
