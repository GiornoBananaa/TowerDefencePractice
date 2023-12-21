using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    [Serializable]
    public class UnityAnimationEvent : UnityEvent<string>{};
    [RequireComponent(typeof(Animator))]
    public class AnimationEventDispatcher : MonoBehaviour
    {
        public UnityAnimationEvent OnAnimationStart;
        public UnityAnimationEvent OnAnimationComplete;
        public UnityAnimationEvent OnAnimationCustomEvent;
    
        [SerializeField] private Animator animator;
    
        void Awake()
        {
            animator = GetComponent<Animator>();
            for(int i=0; i<animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];
            
                AnimationEvent animationStartEvent = new AnimationEvent();
                animationStartEvent.time = 0;
                animationStartEvent.functionName = "AnimationStartHandler";
                animationStartEvent.stringParameter = clip.name;
            
                AnimationEvent animationEndEvent = new AnimationEvent();
                animationEndEvent.time = clip.length;
                animationEndEvent.functionName = "AnimationCompleteHandler";
                animationEndEvent.stringParameter = clip.name;
            
                clip.AddEvent(animationStartEvent);
                clip.AddEvent(animationEndEvent);
            }
        }

        public void AnimationCustomHandler(string name)
        {
            OnAnimationCustomEvent?.Invoke(name);
        }
        
        public void AnimationStartHandler(string name)
        {
            OnAnimationStart?.Invoke(name);
        }
        
        public void AnimationCompleteHandler(string name)
        {
            OnAnimationComplete?.Invoke(name);
        }
    }
}