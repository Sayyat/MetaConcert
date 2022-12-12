using Agora;
using Assets.Scripts.UI;
using Photon.Pun;

namespace UI
{
    public class UserButtonsController
    {
        private UserButtonsView _buttons;
        private AgoraView _agoraView;
        private PhotonView _photonView;


        public UserButtonsController(UserButtonsView buttons, AgoraView agoraView, PhotonView photonView)
        {
            _buttons = buttons;
            _agoraView = agoraView;
            _photonView = photonView;
        }

        public void SetupButtons()
        {
            _buttons.CameraBtn.onClick.AddListener(() =>
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
        }
    }
}