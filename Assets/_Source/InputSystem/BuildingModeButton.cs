using System;
using UnityEngine;
using UnityEngine.UI;

namespace InputSystem
{
    public class BuildingModeButton : MonoBehaviour
    {
        [SerializeField] private Sprite _closeSprite;
        [SerializeField] private Sprite _buildSprite;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private bool _isBuildMode;
        public Action OnBuildModeEnable;
        public Action OnBuildModeDisable;
        
        private void Awake()
        {
            _isBuildMode = false;
            _image.sprite = _buildSprite;
            _button.onClick.AddListener(ButtonClick);
        }

        private void ButtonClick()
        {
            if(_isBuildMode)
                OnBuildModeDisable?.Invoke();
            else
                OnBuildModeEnable?.Invoke();
        }
        public void EnableBuildView()
        {
            _isBuildMode = true;
            _image.sprite = _closeSprite;
        }
        
        public void DisableBuildView()
        {
            _isBuildMode = false;
            _image.sprite = _buildSprite;
        }
    }
}
