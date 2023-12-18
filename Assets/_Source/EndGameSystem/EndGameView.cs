using UnityEngine;
using UnityEngine.SceneManagement;

namespace EndGameSystem
{
    public enum EndGameType
    {
       Win, 
       Loss
    }
    
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _loosePanel;
        
        public void OpenEndGamePanel(EndGameType endType)
        {
            switch (endType)
            {
                case EndGameType.Win:
                    _winPanel.SetActive(true);
                    break;
                case EndGameType.Loss:
                    _loosePanel.SetActive(true);
                    break;
            }
        }
        
        public void RestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void NextLevelButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        
        public void MainMenuButton()
        {
            SceneManager.LoadScene(0);
        }
    }
}
