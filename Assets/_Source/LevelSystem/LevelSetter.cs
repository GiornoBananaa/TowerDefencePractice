using System;
using UnityEngine;

namespace LevelSystem
{
    public class LevelSetter
    {
        public Action<LevelData> OnLevelChange;
        private LevelData[] _levelsData;
        private int _level;

        public LevelSetter(LevelData[] levelsData)
        {
            _level = -1;
            _levelsData = levelsData;
        }
        
        public void NextLevel()
        {
            if (_level+1 >= _levelsData.Length) return;
            
            _level++;
            OnLevelChange?.Invoke(_levelsData[_level]);
        }
    }
}