using System;

namespace Core
{
    public interface IKillable
    {
        public event Action OnLifeEnd;
        public void TakeDamage(int damage);
        public void Heal(int hp);
    }
}