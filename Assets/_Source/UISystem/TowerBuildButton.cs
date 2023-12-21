using System;
using TMPro;
using TowerSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UISystem
{
    [RequireComponent(typeof(RectTransform))]
    public class TowerBuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
    {
        [SerializeField] private float _sizeOverMouseModifer;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _costText;
        
        private TowerType _towerType;
        
        public Action<TowerType> OnClick;
        public Action<TowerType> OnTowerMouseEnter;
        public Action OnTowerMouseExit;
        private Vector2 _defaultSize;
        
        private void Awake()
        {
            _defaultSize = _rectTransform.sizeDelta;
        }

        public void SetTowerType(TowerData towerData)
        {
            _towerType = towerData.TowerType;
            _image.sprite = towerData.BuildButtonSprite;
            _costText.text = towerData.Price.ToString();
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
