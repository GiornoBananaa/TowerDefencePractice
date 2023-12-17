using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelSystem
{
    public class LevelTimer : MonoBehaviour
    {
        //TODO: Move timer view logic to its own script
        public Action OnAttackEnd;
        public Action OnAttackStart;
        
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Image _panel;
        [SerializeField] private string _preparationText;
        [SerializeField] private string _attackText;
        [SerializeField] private Color _preparationColor;
        [SerializeField] private Color _attackColor;
        
        private float _preparationTime;
        private float _attackTime;
        private float _timeElapsed;
        private bool _isEnabled = false;
        private bool _isPreparation = false;
        
        private void Update()
        {
            if (!_isEnabled) return;
            _timeElapsed -= Time.deltaTime;
            _timeElapsed = _timeElapsed < 0 ? 0 : _timeElapsed;
            ViewTime();
            CheckTimer();
        }

        private void CheckTimer()
        {
            if (_timeElapsed <= 0)
            {
                if(!_isPreparation)
                {
                    EnableTimer(false);
                    OnAttackEnd?.Invoke();
                }
                else
                {
                    OnAttackStart?.Invoke();
                    _timeElapsed = _attackTime;
                    _isPreparation = false;
                    _descriptionText.text = _attackText;
                    _panel.color = _attackColor;
                }
            }
        }
        private void ViewTime()
        {
            _timeText.text = ((int)_timeElapsed).ToString();
        }
        
        public void OnLevelChange(LevelData levelData)
        {
            _attackTime = levelData.LevelDuration;
            _preparationTime = levelData.PreparationDuration;
            _isPreparation = true;
            _timeElapsed = _preparationTime;
            _descriptionText.text = _preparationText;
            _panel.color = _preparationColor;
            EnableTimer(true);
        }
        
        public void EnableTimer(bool isEnabled)
        {
            _isEnabled = isEnabled;
            _panel.gameObject.SetActive(isEnabled);
            if(isEnabled && !_isPreparation)
            {
                OnAttackStart?.Invoke();
            }
        }
    }
}