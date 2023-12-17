using System;

namespace Core
{
    public interface IKillable
    {
        //TODO: On tower death should turn of enemy attacking
        public event Action OnLifeEnd; 
        public int HP { get; }
        public void TakeDamage(int damage);
        public void Heal(int hp);
    }
}