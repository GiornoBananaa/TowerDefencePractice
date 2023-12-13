using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TowerSystem
{
    public class TowerSpawner
    {
        private Dictionary<TowerType,TowerData> _towersPrefabs;

        public TowerSpawner(Dictionary<TowerType,TowerData> towersPrefabs)
        {
            _towersPrefabs = towersPrefabs;
        }
        
        public void SpawnUnit(TowerType type, Vector3 position, Quaternion rotation, Vector3 attackRangePoint)
        {
            if(!_towersPrefabs.ContainsKey(type))
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
            
            Object.Instantiate(_towersPrefabs[type].Prefab,position,rotation).GetComponent<Tower>().SetRangePoint(attackRangePoint);
        }
    }
}

