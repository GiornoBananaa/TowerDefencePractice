using System;
using InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UISystem
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private RectTransform _panel;

        private void Start()
        {
            _continueButton.onClick.AddListener(OpenButton);
            _pauseButton.onClick.AddListener(OpenButton);
            _mainMenuButton.onClick.AddListener(MainMenuButton);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenButton();
            }
        }

        private void MainMenuButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    
        private void OpenButton()
        {
            _panel.gameObject.SetActive(!_panel.gameObject.activeSelf);
            Time.timeScale = _panel.gameObject.activeSelf? 0:1;
            _inputListener.gameObject.SetActive(!_panel.gameObject.activeSelf);
        }
    }
}
