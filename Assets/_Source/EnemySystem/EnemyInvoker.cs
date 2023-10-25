using UnityEngine;

namespace EnemySystem
{
    public class EnemyInvoker
    {
        private readonly EnemyMovement _enemyMovement;
        private readonly EnemyCombat _enemyCombat;
        
        public EnemyInvoker(EnemyMovement enemyMovement)
        {
            _enemyMovement = enemyMovement;
        }
        
        public void SetNewEnemyTarget(Vector3 target)
        {
            _enemyMovement.SetNewTargetPosition(target);
        }
        
        public void AttackBase()
        {
            _enemyCombat.AttackBase();
        }
        public void StopBaseAttack()
        {
            _enemyCombat.StopBaseAttack();
        }
    }
}
