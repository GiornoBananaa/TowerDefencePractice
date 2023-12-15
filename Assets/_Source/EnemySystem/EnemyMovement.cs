using System.Collections.Generic;
using TowerSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class EnemyMovement
    {
        private readonly NavMeshAgent _navMeshAgent;
        private List<Transform> _towersInRange;
        
        public EnemyMovement(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
            _towersInRange = new List<Transform>();
        }

        public void AddTarget(Transform target)
        {
            _towersInRange.Add(target);
        }
        
        public void RemoveTarget(Transform target)
        {
            _towersInRange.Remove(target);
        }
        
        public void SetNewTargetPosition(Vector3 target)
        {
            _navMeshAgent.SetDestination(target);
        }
    }
}
