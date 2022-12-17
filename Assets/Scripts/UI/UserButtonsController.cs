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
            
            _buttons.Hello.onClick.AddListener(() => SetCustomPropertiesDance(4));
            _buttons.Applause.onClick.AddListener(() => SetCustomPropertiesDance(5));
            _buttons.Dance1.onClick.AddListener(() => SetCustomPropertiesDance(1));
            _buttons.Dance2.onClick.AddListener(() => SetCustomPropertiesDance(2));
            _buttons.Dance3.onClick.AddListener(() => SetCustomPropertiesDance(3));
            
            
        }

        private void SetCustomPropertiesDance(int id)
        {
            var customProperties = _photonView.Owner.CustomProperties;
            if (customProperties.ContainsKey("DanceID"))
            {
                customProperties["DanceID"] = id;
            }
            else
            {
                customProperties.Add("DanceID", id);
            }

            _photonView.Owner.SetCustomProperties(customProperties);
        }
        
        public void RemoveAllListeners()
        {
            _buttons.Microphone.onClick.RemoveListener(_agoraView.ToggleAudio);
            _buttons.Quit.onClick.RemoveListener(_agoraView.Quit);
            
            
        }
    }
}