using System;
using System.Collections.Generic;
using System.Linq;
using PlayerSystem;
using TowerSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace UISystem
{
    public class TowerOptionsUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Transform _buttonsLayout;
        [SerializeField] private GameObject _buttonPrefab;
        
        private PlayerInvoker _playerInvoker;
        private List<TowerBuildButton> _activeTowerBuildButtons;
        private Queue<TowerBuildButton> _towerBuildButtonPool;
        private Dictionary<TowerType,TowerData> _towerDatas;
        
        public void Construct(PlayerInvoker playerInvoker, Dictionary<TowerType,TowerData> towerDatas)
        {
            _playerInvoker = playerInvoker;
            _playerInvoker.OnTowerCellSelect += OpenPanel;
            _activeTowerBuildButtons = new List<TowerBuildButton>();
            _towerBuildButtonPool = new Queue<TowerBuildButton>();
            _towerDatas = towerDatas;
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
        
        private void OpenPanel(TowerType[] towerTypes)
        {
            foreach (var towerType in towerTypes)
            {
                if(_towerBuildButtonPool.Count!=0)
                {
                    TowerBuildButton button = _towerBuildButtonPool.Dequeue();
                    button.SetTowerType(towerType, _towerDatas[towerType].Price);
                    button.gameObject.SetActive(true);
                    _activeTowerBuildButtons.Add(button);
                }
                else
                {
                    TowerBuildButton towerBuildButton =
                        Instantiate(_buttonPrefab, _buttonsLayout).GetComponent<TowerBuildButton>();

                    towerBuildButton.SetTowerType(towerType, _towerDatas[towerType].Price);
                    towerBuildButton.OnClick += SpawnUnit;
                    towerBuildButton.OnTowerMouseEnter += TurnOnTowerPreview;
                    towerBuildButton.OnTowerMouseExit += TurnOffTowerPreview;

                    _activeTowerBuildButtons.Add(towerBuildButton);
                }
            }
            _panel.SetActive(true);
        }

        private void OnDisable()
        {
            foreach (var towerBuildButton in _activeTowerBuildButtons)
            {
                towerBuildButton.OnClick -= SpawnUnit;
                towerBuildButton.OnTowerMouseEnter -= TurnOnTowerPreview;
                towerBuildButton.OnTowerMouseExit -= TurnOffTowerPreview;
                towerBuildButton.gameObject.SetActive(false);
                _towerBuildButtonPool.Enqueue(towerBuildButton);
            }
            _activeTowerBuildButtons.Clear();
        }
    }
}
