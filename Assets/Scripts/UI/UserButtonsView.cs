using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.UI;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace UI
{
    public class UserButtonsView : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button quit;
        [SerializeField] private Button playerSettings;
        [SerializeField] private Button likes;
        [SerializeField] private TMP_Text likesCount;
        [SerializeField] private Button screenshot;
        [SerializeField] private Button mobileUIToggle;
        [SerializeField] private Button microphone;
        [SerializeField] private Button video;
        [SerializeField] private Button chat;
        [SerializeField] private Button profile;
        [SerializeField] private Button help;
        [SerializeField] private Button dance;

        [SerializeField] private DancePanel dancePanel;


        public Button Quit => quit;
        public Button PlayerSettings => playerSettings;
        public Button Likes => likes;
        public TMP_Text LikesCount => likesCount;
        public Button Screenshot => screenshot;
        public Button MobileUIToggle => mobileUIToggle;
        public Button Microphone => microphone;
        public Button Video => video;
        public Button Chat => chat;
        public Button Profile => profile;
        public Button Help => help;
        public Button Dance => dance;
        public Button Hello => dancePanel.Hello;
        public Button Applause => dancePanel.Applause;
        public Button Dance1 => dancePanel.Dance1;
        public Button Dance2 => dancePanel.Dance2;
        public Button Dance3 => dancePanel.Dance3;

        private List<Button> _buttons = new List<Button>();


        private const string Pattern = @"yyyyMMddHHmmssfff";


        private void Start()
        {
            InitialiseButtons();
            likesCount.text = "ZIZ";
        }

        private void InitialiseButtons()
        {
            _buttons.Add(quit);
            _buttons.Add(playerSettings);
            _buttons.Add(likes);
            _buttons.Add(screenshot);
            _buttons.Add(mobileUIToggle);
            _buttons.Add(microphone);
            _buttons.Add(video);
            _buttons.Add(chat);
            _buttons.Add(profile);
            _buttons.Add(help);
            _buttons.Add(dance);
            _buttons.Add(dancePanel.Hello);
            _buttons.Add(dancePanel.Applause);
            _buttons.Add(dancePanel.Dance1);
            _buttons.Add(dancePanel.Dance2);
            _buttons.Add(dancePanel.Dance3);

            foreach (var button in _buttons)
            {
                var controller = button.gameObject.AddComponent<BtnViewController>();
                controller.CurrentButton = button;
                controller.percentScale = 10f;
            }

            Screenshot.onClick.AddListener(TakeScreenshot);
            Quit.onClick.AddListener(LeaveRoom);
            dancePanel.Init();
            Dance.onClick.AddListener(ToggleDancePanel);
        }

        private void TakeScreenshot()
        {
            var date = DateTime.Now.ToString(Pattern);
            var path = Application.persistentDataPath + $"/{date}.png";
            ScreenCapture.CaptureScreenshot(path);
            Debug.Log($"Image saved in : {path}");
        }


        private void ToggleDancePanel()
        {
            dancePanel.Toggle();
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
            Screenshot.onClick.RemoveListener(TakeScreenshot);
            Quit.onClick.RemoveListener(LeaveRoom);
            Dance.onClick.RemoveListener(ToggleDancePanel);
        }
    }
}