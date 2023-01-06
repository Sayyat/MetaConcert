using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace PlayerControl
{
    public class AnimationControl : MonoBehaviourPunCallbacks
    {
        private int _animIDDance;
        private int _animIDIsDancing;

        private Animator _animator;
        private bool _hasAnimator;

        private void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _animIDDance = Animator.StringToHash("Dance");
            _animIDIsDancing = Animator.StringToHash("IsDancing");
        }


        private void Update()
        {
            if (!photonView.IsMine) return;
            _hasAnimator = TryGetComponent(out _animator);

            HandleKeyBoard();
        }

        private void HandleKeyBoard()
        {
            if (Input.anyKeyDown)
            {
                StartDance(0);
            }

            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartDance(1);
            }

            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartDance(2);
            }

            if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartDance(3);
            }

            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                StartDance(4);
            }

            if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                StartDance(5);
            }
        }

        public void StartDance(int id)
        {
            StartCoroutine(Dance(id));

        }
        

        private IEnumerator Dance(int dance)
        {
            // update animator if using character
            if (!_hasAnimator) yield break;

            _animator.SetInteger(_animIDDance, dance);
            _animator.SetBool(_animIDIsDancing, true);

            yield return new WaitForSeconds(0.1f);

            _animator.SetBool(_animIDIsDancing, false);
        }
    }
}