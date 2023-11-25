using System;
using TMPro;
using UnityEngine;

namespace LevelSystem
{
    public class LevelTimer : MonoBehaviour
    {
        public Action OnAttackEnd;
        public Action OnAttackStart;
        
        [SerializeField] private TMP_Text _timeText;
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
                }
            }
        }
        private void ViewTime()
        {
            _timeText.text = _timeElapsed.ToString("F").Replace(",", ":");
        }
        
        public void OnLevelChange(LevelData levelData)
        {
            _attackTime = levelData.LevelDuration;
            _preparationTime = levelData.PreparationDuration;
            _isPreparation = true;
            _timeElapsed = _preparationTime;
            EnableTimer(true);
        }
        
        public void EnableTimer(bool isEnabled)
        {
            _isEnabled = isEnabled;
            _timeText.gameObject.SetActive(isEnabled);
            if(isEnabled && !_isPreparation)
            {
                OnAttackStart?.Invoke();
            }
        }
    }
}