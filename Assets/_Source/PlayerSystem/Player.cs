using UnityEngine;
using UnityEngine.AI;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator{ get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent{ get; private set; }
    }
}
