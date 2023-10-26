namespace EnemySystem
{
    public class EnemyHealth
    {
        private readonly Enemy _enemy;
        private readonly int _maxHp;
        private int _currentHp;
        
        public EnemyHealth(int maxHp, Enemy enemy)
        {
            _maxHp = maxHp;
            _currentHp = maxHp;
            _enemy = enemy;
        }
        
        public void TakeDamage(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
            {
                _enemy.OnLifeEnd.Invoke();
            }

            if (_currentHp < 0)
                _currentHp = 0;
        }
        
        public void Heal(int hp)
        {
            _currentHp = _currentHp + hp > _maxHp ?
                _maxHp : _currentHp + hp;
        }
    }
}
