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
        
        
        private void Awake()
        {
            _outline.OutlineWidth = _defaultWidth;
            _outlineHighlighted = false;
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
            
            _outline.OutlineWidth = _defaultWidth;
        }
        
        public void EnableOutline(bool enable)
        {
            StartCoroutine(OutlineEnable(enable));
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
