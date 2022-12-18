using System.Collections;
using UnityEngine;

namespace Bot
{
    public class BotAnimationControl : MonoBehaviour
    {
        private int _animIDGreeting;

        private Animator _animator;
        private bool _hasAnimator;

        private void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _animIDGreeting = Animator.StringToHash("IsGreeting");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                StartCoroutine(WaveHand());
            }
        }

        private IEnumerator WaveHand()
        {
            _animator.SetBool(_animIDGreeting, true);
            yield return new WaitForSeconds(0.1f);
            _animator.SetBool(_animIDGreeting, false);
        }
    }
}