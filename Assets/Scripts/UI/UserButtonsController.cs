using Agora;
using Assets.Scripts.UI;
using Photon.Pun;
using PlayerControl;
using UnityEngine;

namespace UI
{
    public class UserButtonsController
    {
        private UserButtonsView _buttons;
        private AgoraView _agoraView;
        private PhotonView _photonView;
        private AnimationControl _animationControl;


        public UserButtonsController(UserButtonsView buttons, AgoraView agoraView, PhotonView photonView,
            AnimationControl animationControl)
        {
            _buttons = buttons;
            _agoraView = agoraView;
            _photonView = photonView;
            _animationControl = animationControl;
        }

        public void SetupButtons()
        {
            _buttons.Video.onClick.AddListener(() =>
            {
                var state = _agoraView.ToggleVideo();
                var customProperties = _photonView.Owner.CustomProperties;
                if (customProperties.ContainsKey("IsVideoOn"))
                {
                    customProperties["IsVideoOn"] = state;
                }
                else
                {
                    customProperties.Add("IsVideoOn", state);
                }

                _photonView.Owner.SetCustomProperties(customProperties);
            });
            _buttons.Microphone.onClick.AddListener(_agoraView.ToggleAudio);
            _buttons.Quit.onClick.AddListener(_agoraView.Quit);
            
            _buttons.Hello.onClick.AddListener(() => _animationControl.StartDance(4));
            _buttons.Applause.onClick.AddListener(() => _animationControl.StartDance(5));
            _buttons.Dance1.onClick.AddListener(() => _animationControl.StartDance(1));
            _buttons.Dance2.onClick.AddListener(() => _animationControl.StartDance(2));
            _buttons.Dance3.onClick.AddListener(() => _animationControl.StartDance(3));
        }
        public void RemoveAllListeners()
        {
            _buttons.Hello.onClick.RemoveAllListeners();
            _buttons.Applause.onClick.RemoveAllListeners();
            _buttons.Dance1.onClick.RemoveAllListeners();
            _buttons.Dance2.onClick.RemoveAllListeners();
            _buttons.Dance3.onClick.RemoveAllListeners();
            _buttons.Microphone.onClick.RemoveListener(_agoraView.ToggleAudio);
            _buttons.Quit.onClick.RemoveListener(_agoraView.Quit);
        }
    }
}