using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LevelSystem
{
    public class LevelTimer : MonoBehaviour
    {
        //TODO: Move timer view logic to its own script
        public Action OnAttackEnd;
        public Action OnAttackStart;
        
        [SerializeField] private Slider _timeSlider;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private string _preparationText;
        [SerializeField] private string _attackText;
        
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
                }
            }
        }
        private void ViewTime()
        {
            float maxTime = _isPreparation ? _preparationTime:_attackTime;
            _timeSlider.value = _timeElapsed/maxTime;
        }
        
        public void OnWaveChange(LevelData levelData)
        {
            _attackTime = levelData.LevelDuration;
            _preparationTime = levelData.PreparationDuration;
            _isPreparation = true;
            _timeElapsed = _preparationTime;
            if(_descriptionText!=null)
            {
                _descriptionText.text = _preparationText;
                EnableTimer(true);
            }
        }
        
        public void EnableTimer(bool isEnabled)
        {
            _isEnabled = isEnabled;
            if(isEnabled && !_isPreparation)
            {
                OnAttackStart?.Invoke();
            }
        }
    }
}