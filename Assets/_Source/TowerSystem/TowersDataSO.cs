using UnityEngine;

namespace TowerSystem
{
    [CreateAssetMenu(fileName = "new TowersData", menuName = "SO/TowersData")]
    public class TowersDataSO : ScriptableObject
    {
        [field: SerializeField] public TowerData[] BasicSquirrelDataByLevels { get; private set; }
        [field: SerializeField] public TowerData[] Squirrel2DataByLevels { get; private set; }
        [field: SerializeField] public BerserkTowerData[] BerserkSquirrelDataByLevels { get; private set; }
    }
    
    //TODO: Upgrades data
}
