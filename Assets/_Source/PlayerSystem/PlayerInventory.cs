using System;

namespace PlayerSystem
{
    public class PlayerInventory
    {
        private int _coins;
        
        public Action<int> OnCoinsCountChange;

        public int Coins
        {
            get => _coins;
            private set
            {
                _coins = value;
                OnCoinsCountChange?.Invoke(_coins);
            }
        }
        
        public PlayerInventory()
        {
            Coins = 5;
        }
        
        public void AddCoins(int count)
        {
            Coins+=count;
        }
        
        public bool SpendCoins(int count)
        {
            if (Coins < count)
                return false;
            
            Coins-=count;
            return true;
        }
    }
}
