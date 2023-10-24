using UnityEngine;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField] public Animator Speed{ get; private set; }
        [field: SerializeField] public Animator Attack{ get; private set; }
        [field: SerializeField] public Animator Hp{ get; private set; }
    }
}
