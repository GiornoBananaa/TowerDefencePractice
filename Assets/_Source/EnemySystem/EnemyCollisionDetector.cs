using System;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyCollisionDetector : MonoBehaviour
    {
        private int _enemyDeadlyLayer;
        private EnemyInvoker _enemyInvoker;
        
        public void Construct(EnemyInvoker enemyInvoker, int enemyDeadlyLayerMask)
        {
            _enemyInvoker = enemyInvoker;
            _enemyDeadlyLayer = enemyDeadlyLayerMask;
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.layer == _enemyDeadlyLayer)
            {
                _enemyInvoker.ReturnToPool();
            }
        }
    }
}
