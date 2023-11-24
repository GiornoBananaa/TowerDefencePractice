using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace LevelSystem
{
    public class LevelTimer : MonoBehaviour
    {
        public Action OnTimerEnd;
        public Action OnTimerStart;
        
        [SerializeField] private TMP_Text _timeText;
        private float _timeElapsed;
        private bool _isEnabled;

        private void Awake()
        {
            _isEnabled = false;
        }
        
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
                EnableTimer(false);
                OnTimerEnd?.Invoke();
            }
        }
        private void ViewTime()
        {
            _timeText.text = _timeElapsed.ToString("F").Replace(",", ":");
        }
        
        public void OnLevelChange(LevelData levelData)
        {
            _timeElapsed = levelData.LevelDuration;
        }
        
        public void EnableTimer(bool isEnabled)
        {
            _isEnabled = isEnabled;
            if(isEnabled)
                OnTimerStart?.Invoke();
        }
    }
}
