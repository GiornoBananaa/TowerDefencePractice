using System;
using UnityEngine;

namespace Core
{
    public class DayAndNightCycle : MonoBehaviour
    {
        [SerializeField] private float _dayDuration;
        [SerializeField] private float _nightDuration;
        [SerializeField] private Light _light;
        
        private float _timeElapsed;
        private bool _isDay;
        private float _currentDuration;
        private float _currentLightIntensity;
        private float _currentLightAngle;
        private float _currentLightRotationSpeed;

        public Action OnDayStarted;
        public Action OnNightStarted;

        private void Update()
        {
            CheckTime();
            RotateLight();
        }

        private void CheckTime()
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= _currentDuration)
            {
                _timeElapsed = 0;
                _isDay = !_isDay;
                if (_isDay)
                {
                    _currentDuration = _dayDuration;
                    _currentLightIntensity = 1;
                    _currentLightAngle = 180;
                    _currentLightRotationSpeed = 360 / _dayDuration;
                }
                else
                {
                    _currentDuration = _nightDuration;
                    _currentLightIntensity = 0;
                    _currentLightAngle = 0;
                    _currentLightRotationSpeed = -360 / _nightDuration;
                }
            }
        }

        private void RotateLight()
        {
            _light.intensity = Mathf.Lerp(
                _light.intensity,
                _currentLightIntensity,
                Time.deltaTime);
            _light.transform.Rotate(
                _currentLightRotationSpeed * Time.deltaTime,
                0,
                0);
            _light.spotAngle = Mathf.Lerp(
                _light.spotAngle,
                _currentLightAngle,
                Time.deltaTime);
        }
    }
}
