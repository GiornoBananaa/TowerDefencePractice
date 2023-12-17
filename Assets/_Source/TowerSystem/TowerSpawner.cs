using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TowerSystem
{
    public class TowerSpawner
    {
        private Dictionary<TowerType,TowerData> _towersData;

        public TowerSpawner(Dictionary<TowerType,TowerData> towersData)
        {
            _towersData = towersData;
        }
        
        public void SpawnUnit(TowerType type,  TowerCell towerCell)
        {
            if(!_towersData.ContainsKey(type))
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
            
            Object.Instantiate(_towersData[type].Prefab,towerCell.SpawnPoint.position,towerCell.SpawnPoint.localRotation)
                .GetComponent<Tower>().Construct(towerCell,_towersData[type]);
        }
    }
}

