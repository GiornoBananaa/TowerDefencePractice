using UnityEngine;

namespace LevelSystem
{
    [CreateAssetMenu(fileName = "LevelsData",menuName = "SO/LevelsData")]
    public class LevelsDataSO : ScriptableObject
    {
        [field: SerializeField] public LevelData[] LevelsData { get; private set; }
    }
}
