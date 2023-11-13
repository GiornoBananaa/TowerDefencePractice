using PlayerSystem;
using TowerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class TowerOptionsUI : MonoBehaviour
    {
        [SerializeField] private Button _basicButton;
        [SerializeField] private GameObject _panel;

        private PlayerInvoker _playerInvoker;
        
        public void Construct(PlayerInvoker playerInvoker)
        {
            _playerInvoker = playerInvoker;
            _playerInvoker.OnTowerCellSelect += OpenPanel;
        }

        private void Awake()
        {
            _basicButton.onClick.AddListener(() => SpawnUnit(TowerType.Basic));
        }

        private void SpawnUnit(TowerType type)
        {
            _playerInvoker.SpawnUnit(type);
            _panel.SetActive(false);
        }
        
        private void OpenPanel()
        {
            _panel.SetActive(true);
        }
    }
}
