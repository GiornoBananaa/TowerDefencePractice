using System;

namespace PlayerSystem
{
    public class PlayerInventory
    {
        private int _coins;

        public Action<int> OnCoinsCountChange;
        
        public void AddCoins(int count)
        {
            _coins+=count;
            OnCoinsCountChange.Invoke(_coins);
        }
        
        public bool SpendCoins(int count)
        {
            if (_coins < count)
                return false;
            
            _coins-=count;
            OnCoinsCountChange.Invoke(_coins);
            return true;
        }
    }
}
