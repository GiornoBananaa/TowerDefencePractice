using System.Collections.Generic;
using System.Linq;
using Core;
using TowerSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class EnemyMovement
    {
        private readonly NavMeshAgent _navMeshAgent;
        private List<Tower> _additionalTargets;
        private Vector3 _mainTarget;
        private bool _additionalTargeting;
        
        public EnemyMovement(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
            _additionalTargets = new List<Tower>();
            _additionalTargeting = true;
        }

        public void AddTarget(Tower target)
        {
            if (!_additionalTargeting) return;
            _additionalTargets.Add(target);
            _navMeshAgent.SetDestination(_additionalTargets.First().transform.position);
            ((IKillable)target).OnLifeEnd+= () => RemoveTarget(target);
        }
        
        public void RemoveTarget(Tower target)
        {
            if(_navMeshAgent == null)
                return;
            _additionalTargets.Remove(target);
            if (!_additionalTargeting)
            {
                _navMeshAgent.SetDestination(_mainTarget);
                return;
            }
            _navMeshAgent.SetDestination(_additionalTargets.Count == 0 ? 
                _mainTarget : _additionalTargets.First().transform.position);
            ((IKillable)target).OnLifeEnd -= () => RemoveTarget(target);
        }
        
        public void SetNewTargetPosition(Vector3 target)
        {
            _navMeshAgent.SetDestination(target);
            _mainTarget = target;
        }

        public void EnableAdditionalMoveTargeting(bool enable) => _additionalTargeting = enable;
    }
}
