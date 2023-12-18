namespace EndGameSystem
{
    public class Game
    {
        private EndGameView _endGameView;

        public Game(EndGameView endGameView)
        {
            _endGameView = endGameView;
        }
        
        public void Lose()
        {
            _endGameView.OpenEndGamePanel(EndGameType.Loss);
        }
        
        public void Win()
        {
            _endGameView.OpenEndGamePanel(EndGameType.Win);
        }
    }
}
