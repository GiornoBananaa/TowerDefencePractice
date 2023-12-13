using Core;
using TowerSystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyInvoker
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovement _enemyMovement;
        private readonly EnemyCombat _enemyCombat;
        private readonly EnemyHealth _enemyHealth;
        
        public EnemyInvoker(Enemy enemy,EnemyMovement enemyMovement,EnemyCombat enemyCombat,EnemyHealth enemyHealth)
        {
            _enemy = enemy;
            _enemyMovement = enemyMovement;
            _enemyCombat = enemyCombat;
            _enemyHealth = enemyHealth;
        }
        
        public void TakeDamage(int damage) => _enemyHealth.TakeDamage(damage);

        public void StartBaseAttack() => _enemyCombat.StartBaseAttack();

        public void UpdateAttackCooldown()
        {
            _enemyCombat.UpdateCooldown();
        }
        
        public void StopBaseAttack()
        {
            _enemyCombat.StopBaseAttack();
        }
        
        public void AttackTower(Tower tower)
        {
            _enemyCombat.StartTowerAttack((IKillable)tower);
            _enemyMovement.AddTarget(tower.transform);
        }
        
        public void StopTowerAttack(Tower tower)
        {
            _enemyCombat.StopTowerAttack((IKillable)tower);
            _enemyMovement.RemoveTarget(tower.transform);
        }
        
        public void SetNewTargetPosition(Vector3 target)
        {
            _enemyMovement.SetNewTargetPosition(target);
        }
        
        public void ResetEnemy()
        {
            _enemyHealth.Heal(100);
        }
        
        public void ReturnToPool()
        {
            _enemy.OnReturnToPool?.Invoke();
        }
    }
}
