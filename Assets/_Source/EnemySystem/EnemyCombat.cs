using BaseSystem;

namespace EnemySystem
{
    public class EnemyCombat
    {
        private readonly int _attack;
        private bool _baseInAttackRange;
        private BaseHealth _baseHealth;

        public EnemyCombat(BaseHealth baseHealth, int attack)
        {
            _attack = attack;
            _baseHealth = baseHealth;
            _baseInAttackRange = false;
        }
        
        public void AttackBase()
        {
            _baseInAttackRange = true;
            _baseHealth.TakeDamage(_attack);
        }
        
        public void StopBaseAttack()
        {
            _baseInAttackRange = true;
            _baseHealth.TakeDamage(_attack);
        }
    }
}
