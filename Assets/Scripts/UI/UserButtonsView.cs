using System;
using System.Collections.Generic;
using Assets.Scripts.UI;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UserButtonsView : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button likes;
        [SerializeField] private TMP_Text likesCount;
        [SerializeField] private Button screenshot;
        [SerializeField] private Button playerSettings;
        [SerializeField] private Button quit;
        [SerializeField] private Button help;
        [SerializeField] private Button more;
        [SerializeField] private Button microphone;
        [SerializeField] private Button cameraBtn;
        [SerializeField] private Button chat;
        [SerializeField] private Button mobileUIToggle;


        public Button Likes => likes;
        public TMP_Text LikesCount => likesCount;
        public Button Screenshot => screenshot;
        public Button PlayerSettings => playerSettings;
        public Button Quit => quit;
        public Button Help => help;
        public Button More => more;
        public Button Microphone => microphone;
        public Button CameraBtn => cameraBtn;
        public Button Chat => chat;
        public Button MobileUIToggle => mobileUIToggle;

        private List<Button> _buttons = new List<Button>();


        private const string Pattern = @"yyyyMMddHHmmssfff";

        
        
        
        
        private void Start()
        {
            InitialiseButtons();
            likesCount.text = "ZIZ";
        }

        private void InitialiseButtons()
        {
            _buttons.Add(likes);
            _buttons.Add(screenshot);
            _buttons.Add(playerSettings);
            _buttons.Add(quit);
            _buttons.Add(help);
            _buttons.Add(more);
            _buttons.Add(chat);
            _buttons.Add(cameraBtn);
            _buttons.Add(microphone);
            _buttons.Add(mobileUIToggle);

            foreach (var button in _buttons)
            {
                var controller = button.gameObject.AddComponent<BtnViewController>();
                controller.CurrentButton = button;
                controller.percentScale = 10f;
            }

            Screenshot.onClick.AddListener(TakeScreenshot);
            Quit.onClick.AddListener(LeaveRoom);
        }

        private void TakeScreenshot()
        {
            var date = DateTime.Now.ToString(Pattern);
            var path = Application.persistentDataPath + $"/{date}.png";
            ScreenCapture.CaptureScreenshot(path);
            Debug.Log($"Image saved in : {path}");
            
        }


        private void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            // SceneManager.LoadSceneAsync(0);
            PhotonNetwork.LoadLevel(0);

            // unsubscribe events
            Screenshot.onClick.AddListener(TakeScreenshot);
            Quit.onClick.AddListener(LeaveRoom);
        }
    }
}