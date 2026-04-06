using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DanceBattle
{
    public class PlayerAnimator : MonoBehaviour
    {
        private readonly int[] _specialDances =
        {
            AnimatortParameters.SpecialDance1,
            AnimatortParameters.SpecialDance2,
        };

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private Animator _animator;

        public void PlaySpecial()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                _animator.SetTrigger(_specialDances[Random.Range(0, _specialDances.Length)]);
            }
        }

        public bool IsPlayingSpecial()
        {
            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
            if (IsSpecialState(currentState))
            {
                return true;
            }

            if (_animator.IsInTransition(0))
            {
                AnimatorStateInfo next = _animator.GetNextAnimatorStateInfo(0);
                return IsSpecialState(next);
            }

            return false;
        }

        private bool IsSpecialState(AnimatorStateInfo state)
        {
            return state.IsName("SpecialDance1") || state.IsName("SpecialDance2");
        }
    }
}