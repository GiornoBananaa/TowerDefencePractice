using UnityEngine;

namespace Audio
{
    public class MusicSetter : MonoBehaviour
    {
        [SerializeField] private string _soundName;
        private void Start()
        {   
            AudioManager.Instance.Play(_soundName);
        }
    }
}
