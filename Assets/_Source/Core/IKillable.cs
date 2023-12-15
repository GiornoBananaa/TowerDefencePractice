namespace Core
{
    public interface IKillable
    {
        public int HP { get; }
        public void TakeDamage(int damage);
        public void Heal(int hp);
    }
}