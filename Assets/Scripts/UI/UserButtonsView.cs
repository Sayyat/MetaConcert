using System;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UserButtonsView : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button likes;
        [SerializeField] private TMP_Text likesCount;
        [SerializeField] private Button videoSettings;
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
        public Button VideoSettings => videoSettings;
        public Button PlayerSettings => playerSettings;
        public Button Quit => quit;
        public Button Help => help;
        public Button More => more;
        public Button Microphone => microphone;
        public Button CameraBtn => cameraBtn;
        public Button Chat => chat;
        public Button MobileUIToggle => mobileUIToggle;
        
        private List<Button> _buttons = new List<Button>();
        private void Start()
        {
            InitialiseButtons();
            likesCount.text = "ZIZ";
        }

        private void InitialiseButtons()
        {
           _buttons.Add(likes);
           _buttons.Add(videoSettings);
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

           Quit.onClick.AddListener(LeaveRoom);
       
        }


        private void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            // SceneManager.LoadSceneAsync(0);
            PhotonNetwork.LoadLevel(0);
        }
        
    }
}
