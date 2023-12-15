using System;
using TowerSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem
{
    [RequireComponent(typeof(RectTransform))]
    public class TowerBuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
    {
        [SerializeField] private TowerType _towerType;
        [SerializeField] private float _sizeOverMouseModifer;
        [SerializeField] private RectTransform _rectTransform;
        
        public Action<TowerType> OnClick;
        public Action<TowerType> OnTowerMouseEnter;
        public Action OnTowerMouseExit;
        private Vector2 _defaultSize;
        
        
        private void Awake()
        {
            _defaultSize = _rectTransform.sizeDelta;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnTowerMouseEnter?.Invoke(_towerType);
            _rectTransform.sizeDelta = _defaultSize * _sizeOverMouseModifer;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            
            OnClick?.Invoke(_towerType);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            OnTowerMouseExit?.Invoke();
            _rectTransform.sizeDelta = _defaultSize;
        }
        
        private void OnDisable()
        {
            _rectTransform.sizeDelta = _defaultSize;
        }
    }
}
