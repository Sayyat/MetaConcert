using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
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

            if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetCustomPropertiesDance(4);
            }

            if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
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

            if (!changedProps.ContainsKey("DanceID")) return;

            var danceID = Convert.ToInt32(changedProps["DanceID"]);

            Dance(danceID);
        }


        private void Dance(int dance)
        {
            // update animator if using character
            if (!_hasAnimator) return;
            _animator.SetInteger(_animIDDance, dance);
            
            Debug.Log($"Dance ID: {dance}");
        }
    }
}