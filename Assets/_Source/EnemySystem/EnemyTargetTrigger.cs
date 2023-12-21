using System;
using Core;
using TowerSystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyTargetTrigger : MonoBehaviour
    {
        private int _baseLayer;
        private int _towerLayer;
        private EnemyInvoker _enemyInvoker;

        private int _targetsInRange;
        
        public void Construct(EnemyInvoker enemyInvoker, int baseLayer,int towerLayer)
        {
            _enemyInvoker = enemyInvoker;
            _baseLayer = baseLayer;
            _towerLayer = towerLayer;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _baseLayer)
            {
                _enemyInvoker.StartBaseAttack();
                _targetsInRange++;
            }
            if (other.gameObject.layer == _towerLayer)
            {
                Tower tower = other.gameObject.GetComponent<Tower>();
                if(tower is IKillable)
                {
                    _enemyInvoker.AttackTower(tower);
                    _targetsInRange++;
                }
            }

            if (_targetsInRange > 0)
                _enemyInvoker.PlayAttackAnimation(true);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == _baseLayer)
            {
                _enemyInvoker.StopBaseAttack();
                _targetsInRange--;
            }
            if (other.gameObject.layer == _towerLayer)
            {
                Tower tower = other.gameObject.GetComponent<Tower>();
                if(tower is IKillable)
                {
                    _enemyInvoker.StopTowerAttack(tower);
                    _targetsInRange--;
                }
                
            }
            if (_targetsInRange == 0)
                _enemyInvoker.PlayAttackAnimation(false);
        }

        private void OnDisable()
        {
            _targetsInRange = 0;
            _enemyInvoker.StopBaseAttack();
            _enemyInvoker.PlayAttackAnimation(false);
        }
    }
}
