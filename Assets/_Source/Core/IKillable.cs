using System;

namespace Core
{
    public interface IKillable
    {
        //TODO: On tower death should turn of enemy attacking
        public event Action OnLifeEnd; 
        public void TakeDamage(int damage);
        public void Heal(int hp);
    }
}