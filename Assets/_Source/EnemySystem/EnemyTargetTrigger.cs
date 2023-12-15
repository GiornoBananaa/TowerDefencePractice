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
            }
            if (other.gameObject.layer == _towerLayer)
            {
                Tower tower = other.gameObject.GetComponent<Tower>();
                if(tower is IKillable)
                    _enemyInvoker.AttackTower(tower);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == _baseLayer)
            {
                _enemyInvoker.StopBaseAttack();
            }
            if (other.gameObject.layer == _towerLayer)
            {
                Tower tower = other.gameObject.GetComponent<Tower>();
                if(tower is IKillable)
                    _enemyInvoker.StopTowerAttack(tower);
            }
        }
    }
}
