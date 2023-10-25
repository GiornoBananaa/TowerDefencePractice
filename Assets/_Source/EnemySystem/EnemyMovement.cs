using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class EnemyMovement
    {
        private readonly NavMeshAgent _navMeshAgent;
        
        public EnemyMovement(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }
        
        public void SetNewTargetPosition(Vector3 target)
        {
            _navMeshAgent.SetDestination(target);
        }
    }
}
