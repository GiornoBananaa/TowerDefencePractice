using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TowerSystem
{
    public class TowerSpawner
    {
        private Dictionary<TowerType,GameObject> _towersSpawnData;
        private Dictionary<TowerType,TowerData[]> _towersData;

        public TowerSpawner(Dictionary<TowerType,TowerData[]> towersData,Dictionary<TowerType,GameObject> towersSpawnData)
        {
            _towersData = towersData;
            _towersSpawnData = towersSpawnData;
        }
        
        public void SpawnUnit(TowerType type,  TowerCell towerCell)
        {
            if(!_towersData.ContainsKey(type))
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
            
            Object.Instantiate(_towersSpawnData[type],towerCell.SpawnPoint.position,towerCell.SpawnPoint.localRotation)
                .GetComponent<Tower>().Construct(towerCell,_towersData[type]);
        }
    }
}

