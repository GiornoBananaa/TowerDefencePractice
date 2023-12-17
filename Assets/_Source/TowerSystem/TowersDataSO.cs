using UnityEngine;

namespace TowerSystem
{
    [CreateAssetMenu(fileName = "new TowersData", menuName = "SO/TowersData")]
    public class TowersDataSO : ScriptableObject
    {
        [field: SerializeField] public TowerData BasicSquirrelData { get; private set; }
        [field: SerializeField] public TowerData Squirrel2Data { get; private set; }
        [field: SerializeField] public BerserkTowerData BerserkSquirrelData { get; private set; }
    }
}
