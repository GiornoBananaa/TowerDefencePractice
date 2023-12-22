using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _soundVolumeSlider;
        [SerializeField] private Toggle _musicMuteToggle;
        [SerializeField] private Toggle _soundMuteToggle;

        private void Awake()
        {
            _musicVolumeSlider.value = AudioManager.Instance.MusicVolume;
            _soundVolumeSlider.value = AudioManager.Instance.SoundVolume;
            
            _musicVolumeSlider.onValueChanged.AddListener(MusicChange);
            _soundVolumeSlider.onValueChanged.AddListener(SoundChange);
            _musicMuteToggle.onValueChanged.AddListener(EnableMusic);
            _soundMuteToggle.onValueChanged.AddListener(EnableSound);
        }

        private void SoundChange(float volume)
        {
            AudioManager.Instance.ChangeSoundVolume(volume);
        }
    
        private void MusicChange(float volume)
        {
            AudioManager.Instance.ChangeMusicVolume(volume);
        }
        
        private void EnableMusic(bool enable)
        {
            AudioManager.Instance.EnableMusic(enable);
        }
    
        private void EnableSound(bool enable)
        {
            AudioManager.Instance.EnableSound(enable);
        }
    }
}
