using System;
using Core;
using TowerSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyInvoker
    {
        private static readonly int _deathAnimationHash = Animator.StringToHash("Death");
        private static readonly int _attackAnimationHash = Animator.StringToHash("Attack");
        
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
            _enemyHealth.OnLifeEnd += PlayDeathAnimation;
            Debug.Log(_enemy.AnimationEventDispatcher == null);
            Debug.Log(_enemy.AnimationEventDispatcher.OnAnimationComplete == null);
            _enemy.AnimationEventDispatcher.OnAnimationComplete.AddListener(Death);
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
            _enemyCombat.StartTowerAttack(tower);
            _enemyMovement.AddTarget(tower);
        }
        
        public void StopTowerAttack(Tower tower)
        {
            _enemyCombat.StopTowerAttack(tower);
            _enemyMovement.RemoveTarget(tower);
        }
        
        public void SetNewTargetPosition(Vector3 target)
        {
            _enemyMovement.SetNewTargetPosition(target);
        }
        
        public void ResetEnemy()
        {
            _enemyHealth.Heal(100);
            _enemy.Reset();
            _enemy.Animator.Rebind();
            _enemy.Animator.Update(0f);
        }
        
        public void ReturnToPool()
        {
            _enemy.OnReturnToPool?.Invoke();
        }

        public void EnableAdditionalMoveTargeting(bool enable)
        {
            _enemyMovement.EnableAdditionalMoveTargeting(enable);
        }

        public void PlayDeathAnimation()
        {
            _enemy.Animator.SetTrigger(_deathAnimationHash);
            _enemyMovement.SetNewTargetPosition(_enemy.transform.position);
        }
        
        public void PlayAttackAnimation(bool attack)
        {
            _enemy.Animator.SetBool(_attackAnimationHash,attack);
        }
        
        private void Death(string animationName)
        {
            if (animationName.Substring(animationName.Length-5, 5) != "Death") return;
            ResetEnemy();
            _enemy.DropCoins();
            ReturnToPool();
        }
    }
}
