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
        
        public void SpawnUnit(TowerType type, Vector3 position, Quaternion rotation, Vector3 attackRangePoint)
        {
            if(!_towersData.ContainsKey(type))
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
            
            Object.Instantiate(_towersData[type].Prefab,position,rotation)
                .GetComponent<Tower>().Construct(attackRangePoint,_towersData[type]);
        }
    }
}

