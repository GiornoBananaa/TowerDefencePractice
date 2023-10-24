using UnityEngine;
using UnityEngine.AI;

namespace PlayerSystem
{
    public class PlayerMovement
    {
        private readonly NavMeshAgent _navMeshAgent;

        public PlayerMovement(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public void SetNewPosition(RaycastHit hitInfo)
        {
            _navMeshAgent.SetDestination(hitInfo.point);
        }
    }
}
