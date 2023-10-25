using UnityEngine;

namespace EnemySystem
{
    public class EnemyTargetTrigger : MonoBehaviour
    {
        private int _baseLayer;
        private EnemyInvoker _enemyInvoker;
        
        public void Construct(EnemyInvoker enemyInvoker, int baseLayer)
        {
            _enemyInvoker = enemyInvoker;
            _baseLayer = baseLayer;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _baseLayer)
            {
                _enemyInvoker.AttackBase();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == _baseLayer)
            {
                _enemyInvoker.StopBaseAttack();
            }
        }
    }
}
