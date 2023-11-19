using System;
using System.Collections;
using UnityEngine;

namespace UISystem
{
    [RequireComponent(typeof(Outline))]
    public class ObjectOutlineControl : MonoBehaviour
    {
        [SerializeField] private Outline _outline;
        
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
            
            _outline.OutlineWidth = _defaultWidth;
        }
        
        private void OnMouseExit()
        {
            _outlineViewed = false;
            
            _outline.OutlineWidth = 0;
        }
        
        public void EnableOutline(bool enable)
        {
            StartCoroutine(OutlineEnable(enable));
        }
        
        // This coroutine fixes graphic bug where outline is showed for one frame when it's disabled
        private IEnumerator OutlineEnable(bool enable)
        {
            _outline.OutlineWidth = _outlineViewed && enable ? _defaultWidth : 0;
            yield return new WaitForEndOfFrame();
            _outline.enabled = enable;
        } 
    }
}
