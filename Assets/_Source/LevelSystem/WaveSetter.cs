using System;
using Core;
using EndGameSystem;
using UnityEngine;

namespace LevelSystem
{
    public class WaveSetter
    {
        public Action<LevelData> OnWaveChange;
        private LevelsDataSO _levelsData;
        private Game _game;
        private int _level;
        
        public WaveSetter(LevelsDataSO levelsData, Game game)
        {
            _levelsData = levelsData;
            _game = game;
        }
        
        public void SetWave()
        {
            OnWaveChange?.Invoke(_levelsData.LevelsData[_level]);
        }
        
        public void NextWave()
        {
            if (_level+1 >= _levelsData.LevelsData.Length)
            {
                _game.Win();
                return;
            }
            
            _level++;
            
            OnWaveChange?.Invoke(_levelsData.LevelsData[_level]);
        }
    }
}