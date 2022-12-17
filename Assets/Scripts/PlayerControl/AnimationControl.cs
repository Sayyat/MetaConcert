using System;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

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
                SetCustomPropertiesDance(0);
            }

            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetCustomPropertiesDance(1);
            }

            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetCustomPropertiesDance(2);
            }

            if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetCustomPropertiesDance(3);
            }

            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetCustomPropertiesDance(4);
            }

            if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                SetCustomPropertiesDance(5);
            }
        }

        private void SetCustomPropertiesDance(int id)
        {
            var customProperties = photonView.Owner.CustomProperties;
            if (customProperties.ContainsKey("DanceID"))
            {
                customProperties["DanceID"] = id;
            }
            else
            {
                customProperties.Add("DanceID", id);
            }

            photonView.Owner.SetCustomProperties(customProperties);
        }


        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (photonView.Controller.ActorNumber != targetPlayer.ActorNumber) return;
            var customProperties = targetPlayer.CustomProperties;
            if (!customProperties.ContainsKey("DanceID")) return;
            var danceID = Convert.ToInt32(customProperties["DanceID"]);
            StartCoroutine(Dance(danceID));
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