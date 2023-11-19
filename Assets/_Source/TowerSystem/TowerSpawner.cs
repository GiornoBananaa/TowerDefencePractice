using UnityEngine;

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
            switch (type)
            {
                case TowerType.Basic:
                    Object.Instantiate(_towersPrefabs[(int)type],position,rotation).GetComponent<Tower>().SetRangePoint(attackRangePoint);
                    break;
            }
        }
    }
}

