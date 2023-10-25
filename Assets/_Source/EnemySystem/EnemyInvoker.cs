using UnityEngine;

namespace EnemySystem
{
    public class EnemyInvoker
    {
        private readonly EnemyMovement _enemyMovementMovement;

        public EnemyInvoker(EnemyMovement enemyMovementMovement)
        {
            _enemyMovementMovement = enemyMovementMovement;
        }
        
        public void SetNewEnemyTarget(Vector3 target)
        {
            _enemyMovementMovement.SetNewTargetPosition(target);
        }
    }
}
