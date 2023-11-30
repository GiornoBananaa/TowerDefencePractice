using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TowerSystem
{
    public class TowerSpawner
    {
        private GameObject[] _towersPrefabs;

        public TowerSpawner(GameObject[] towersPrefabs)
        {
            _towersPrefabs = towersPrefabs;
        }
        
        public void SpawnUnit(TowerType type, Vector3 position, Quaternion rotation, Vector3 attackRangePoint)
        {
            int prefabIndex = (int)type;
            
            if(prefabIndex > _towersPrefabs.Length)
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
            
            Object.Instantiate(_towersPrefabs[(int)type],position,rotation).GetComponent<Tower>().SetRangePoint(attackRangePoint);
        }
    }
}

