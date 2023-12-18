using System;
using UnityEngine;

namespace LevelSystem
{
    public class WaveSetter
    {
        public Action<LevelData> OnWaveChange;
        private LevelsDataSO _levelsData;
        private int _level;
        
        public WaveSetter(LevelsDataSO levelsData)
        {
            _levelsData = levelsData;
        }
        
        public void SetWave()
        {
            OnWaveChange?.Invoke(_levelsData.LevelsData[_level]);
        }
        
        public void NextWave()
        {
            if (_level+1 >= _levelsData.LevelsData.Length) return;

            _level++;
            _levelsData.CurrentLevel = _level;
            
            OnWaveChange?.Invoke(_levelsData.LevelsData[_level]);
        }
    }
}