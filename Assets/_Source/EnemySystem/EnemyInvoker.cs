using BaseSystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyInvoker
    {
        private readonly EnemyMovement _enemyMovement;
        private readonly EnemyCombat _enemyCombat;
        
        public EnemyInvoker(EnemyMovement enemyMovement,EnemyCombat enemyCombat)
        {
            _enemyMovement = enemyMovement;
            _enemyCombat = enemyCombat;
        }
        
        public void SetNewEnemyTarget(Vector3 target)
        {
            _enemyMovement.SetNewTargetPosition(target);
        }
        
        public void StartBaseAttack()
        {
            _enemyCombat.StartBaseAttack();
        }
        public void UpdateCooldown()
        {
            _enemyCombat.UpdateCooldown();
        }
        
        public void StopBaseAttack()
        {
            _enemyCombat.StopBaseAttack();
        }
    }
}
