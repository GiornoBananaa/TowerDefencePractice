using UnityEngine;

namespace LevelSystem
{
    [CreateAssetMenu(fileName = "LevelsData",menuName = "SO/LevelsData")]
    public class LevelsDataSO : ScriptableObject
    {
        public int CurrentLevel;
        [field: SerializeField] public LevelData[] LevelsData { get; private set; }
    }
}
