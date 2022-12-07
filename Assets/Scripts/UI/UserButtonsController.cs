using Agora;
using Assets.Scripts.UI;

namespace UI
{
    public class UserButtonsController
    {
        private UserButtonsView _buttons;
        private AgoraView _agoraView;
        

        public UserButtonsController(UserButtonsView buttons, AgoraView agoraView)
        {
            _buttons = buttons;
            _agoraView = agoraView;
        }

        public void SetupButtons()
        {
            _buttons.CameraBtn.onClick.AddListener(_agoraView.ToggleVideo);
            _buttons.Microphone.onClick.AddListener(_agoraView.ToggleAudio);
            _buttons.Quit.onClick.AddListener(_agoraView.Quit);
            
        }
    }
}