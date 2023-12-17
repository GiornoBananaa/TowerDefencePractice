using UnityEngine;

namespace TowerSystem
{
    [CreateAssetMenu(fileName = "new TowersSpawnData", menuName = "SO/TowersSpawnData")]
    public class TowersSpawnDataSO : ScriptableObject
    {
        [field: SerializeField] public TowersSpawnData[] TowersSpawnData { get; private set; }
    }
}