using System;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerAnimations : MonoBehaviour
    {
        private static readonly int RUNNING_ANIMATION_HASH = Animator.StringToHash("IsRunning");

        [SerializeField] private Player player;
        private Animator _animator;
        private Vector3 _prevPosition;
        private bool _isRunning;

        private void Start()
        {
            _animator = player.Animator;
            if (_animator == null) enabled = false;
            _prevPosition = transform.position;
            _isRunning = false;
        }

        private void Update()
        {
            SetAnimations();
        }
        
        private void SetAnimations()
        {
            float prevdist = Vector3.Distance(transform.position, _prevPosition);
            if (prevdist == 0f && _isRunning)
            {
                _animator.SetBool(RUNNING_ANIMATION_HASH, (false));
                _isRunning = false;
            }
            else if (prevdist != 0f && !_isRunning)
            {
                _animator.SetBool(RUNNING_ANIMATION_HASH, (true));
                _isRunning = true;
            }
            _prevPosition = transform.position;
        }
    }
}
