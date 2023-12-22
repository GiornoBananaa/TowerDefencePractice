using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UISystem
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _sceneIndex;

        private void Start()
        {
            _button.onClick.AddListener(LoadScene);
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}
