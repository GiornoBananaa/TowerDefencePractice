using LevelSystem;
using UnityEngine;

namespace Core
{
    public class Game
    {
        private LevelSetter _levelSetter;

        public Game(LevelSetter levelSetter)
        {
            _levelSetter = levelSetter;
        }

        public void Restart()
        {
            
        }
        
        public void Lose()
        {
            
        }
        
        public void PassLevel()
        {
            _levelSetter.NextLevel();
        }
    }
}
