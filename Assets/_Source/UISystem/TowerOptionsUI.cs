using System;
using PlayerSystem;
using TowerSystem;
using UnityEngine;

namespace UISystem
{
    public class TowerOptionsUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TowerBuildButton[] _towerBuildButtons;
        
        private PlayerInvoker _playerInvoker;
        
        public void Construct(PlayerInvoker playerInvoker)
        {
            _playerInvoker = playerInvoker;
            _playerInvoker.OnTowerCellSelect += OpenPanel;
            foreach (var towerBuildButton in _towerBuildButtons)
            {
                towerBuildButton.OnClick += SpawnUnit;
                towerBuildButton.OnTowerMouseEnter += TurnOnTowerPreview;
                towerBuildButton.OnTowerMouseExit += TurnOffTowerPreview;
            }
        }
        
        private void SpawnUnit(TowerType type)
        {
            if (_playerInvoker.SpawnUnit(type))
            {
                AudioManager.Instance.Play("squirrel_interact");
                _panel.SetActive(false);
                TurnOffTowerPreview();
            }
        }
        
        private void TurnOnTowerPreview(TowerType type)
        {
            _playerInvoker.TurnOnTowerPreview(type);
        }
        
        private void TurnOffTowerPreview()
        {
            _playerInvoker.TurnOffTowerPreview();
        }
        
        private void OpenPanel()
        {
            _panel.SetActive(true);
        }

        private void OnDisable()
        {
            foreach (var towerBuildButton in _towerBuildButtons)
            {
                towerBuildButton.OnClick -= SpawnUnit;
                towerBuildButton.OnTowerMouseEnter -= TurnOnTowerPreview;
                towerBuildButton.OnTowerMouseExit -= TurnOffTowerPreview;
            }
        }
    }
}
