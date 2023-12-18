using System.Collections.Generic;
using PlayerSystem;
using TMPro;
using TowerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class TowerInspector: MonoBehaviour
    {
        //TODO: Upgrade buttons and stats
        [SerializeField] private Projector _rangeProjector;
        [SerializeField] private GameObject _inspectorPanel;
        [SerializeField] private GameObject _statsLayout;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _upgradePriceText;
        [SerializeField] private GameObject _statBlockPrefab;
        [SerializeField] private TMP_Text _towerNameText;
        [SerializeField] private TMP_Text _towerLevelText;
        [SerializeField] private TMP_Text _upgradedLevelText;

        private List<TowerStatBlock> _enabledTowerStats;
        private Queue<TowerStatBlock> _towerStatsPool;
        private Tower _inspectedTower;
        private PlayerInvoker _playerInvoker;

        private void Awake()
        {
            _towerStatsPool = new Queue<TowerStatBlock>();
            _enabledTowerStats = new List<TowerStatBlock>();
            _closeButton.onClick.AddListener(StopInspection);
            _upgradeButton.onClick.AddListener(UpgradeTower);
        }

        public void Construct(PlayerInvoker playerInvoker)
        {
            _playerInvoker = playerInvoker;
        }

        public void InspectTower(Tower tower)
        {
            if (_inspectorPanel.gameObject.activeSelf)
                StopInspection();
            
            _inspectedTower = tower;
            
            switch (tower)
            {
                case BaseSquirrel:
                    TowerStatBlock attackStat = AddStatBlock("Attack",tower.TowerData.Attack.ToString());
                    TowerStatBlock attackRangeStat = AddStatBlock("Attack range",tower.TowerData.AttackRange.ToString());
                    TowerStatBlock attackSpeedkStat = AddStatBlock("Attack speed",(1/tower.TowerData.AttackCooldown).ToString("F1") + "/s");
                    if (tower.TowerLevelDatas.Length > tower.Level + 1)
                    {
                        TowerData nextLevelData = tower.TowerLevelDatas[tower.Level + 1];
                        if(tower.TowerLevelDatas[tower.Level + 1].Attack != tower.TowerLevelDatas[tower.Level].Attack)
                            attackStat.EnableUpgradedValueView(nextLevelData.Attack.ToString());
                        if(tower.TowerLevelDatas[tower.Level + 1].AttackRange != tower.TowerLevelDatas[tower.Level].AttackRange)
                            attackRangeStat.EnableUpgradedValueView(nextLevelData.AttackRange.ToString());
                        if(tower.TowerLevelDatas[tower.Level + 1].AttackCooldown != tower.TowerLevelDatas[tower.Level].AttackCooldown)
                            attackSpeedkStat.EnableUpgradedValueView((1/nextLevelData.AttackCooldown).ToString("F1"));
                    }
                    break;
                case BerserkSquirrel:
                    TowerStatBlock hpStat = AddStatBlock("HP",((BerserkTowerData)tower.TowerData).HP.ToString());
                    if (tower.TowerLevelDatas.Length > tower.Level + 1)
                    {
                        TowerData nextLevelData = tower.TowerLevelDatas[tower.Level + 1];
                        hpStat.EnableUpgradedValueView(((BerserkTowerData)nextLevelData).HP.ToString());
                    }
                    break;
            }

            if (tower.TowerLevelDatas.Length <= tower.Level+1)
            {
                _upgradeButton.gameObject.SetActive(false);
                _upgradedLevelText.gameObject.SetActive(false);
            }
            else
            {
                _upgradePriceText.text = tower.TowerLevelDatas[tower.Level + 1].Price.ToString();
                _upgradedLevelText.text = $"{tower.Level+2} lv.";
            }

            _towerNameText.text = tower.TowerData.Name;
            _towerLevelText.text = $"{tower.Level+1} lv.";
            
            _inspectorPanel.SetActive(true);

            ShowAttackRange(tower.TowerCell.AttackRangePoint, tower.TowerData.AttackRange);
        }
        
        public void StopInspection()
        {
            HideAttackRange();
            _inspectorPanel.SetActive(false);
            foreach (var statBlock in _enabledTowerStats)
            {
                _towerStatsPool.Enqueue(statBlock);
                statBlock.gameObject.SetActive(false);
            }
            _enabledTowerStats.Clear();
        }
        
        public void ShowAttackRange(Vector3 rangePoint, float rangeSize)
        {
            _rangeProjector.transform.position = rangePoint + new Vector3(0,2,0);
            _rangeProjector.orthographicSize = rangeSize;
            _rangeProjector.gameObject.SetActive(true);
        }
        
        public void HideAttackRange()
        {
            _rangeProjector.gameObject.SetActive(false);
        }
        
        private void UpgradeTower()
        {
            if(_playerInvoker.UpgradeTower(_inspectedTower))
            {
                StopInspection();
                InspectTower(_inspectedTower);
            }
        }
        
        private TowerStatBlock AddStatBlock(string name, string value)
        {
            TowerStatBlock towerStatBlock;
            if(_towerStatsPool.Count == 0)
            {
                towerStatBlock = Instantiate(_statBlockPrefab, _statsLayout.transform)
                    .GetComponent<TowerStatBlock>();
            }
            else
            {
                towerStatBlock = _towerStatsPool.Dequeue();
            }
            towerStatBlock.DisableUpgradedValueView();
            towerStatBlock.gameObject.SetActive(true);
            _enabledTowerStats.Add(towerStatBlock);
            towerStatBlock.StatName.text = name;
            towerStatBlock.StatValue.text = value;

            return towerStatBlock;
        }
    }
}
