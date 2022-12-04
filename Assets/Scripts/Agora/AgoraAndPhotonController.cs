using System;
using System.Collections.Generic;
using agora_gaming_rtc;
using Assets.Scripts.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Agora
{
    public class AgoraAndPhotonController : MonoBehaviourPun
    {
        public AgoraView MyAgoraView { get; set; }
        public PhotonView MyPhotonView { get; set; }
        public UserButtonsView ButtonsView { get; set; }

        private AgoraController _agoraController;

        private bool _inAgoraRoom { get; set; } = false;
        private bool _inPhotonRoom { get; set; } = false;
        private uint _selfAgoraId { get; set; } = 0;

        public Dictionary<uint, GameObject> AgoraToUnity;
        public Dictionary<int, GameObject> PhotonToUnity;
        public Hashtable AgoraToPhoton;
        
        
        private void Awake()
        {
            MyAgoraView = gameObject.GetComponent<AgoraView>() ?? gameObject.AddComponent<AgoraView>();

            MyAgoraView.OnJoinedRoom += OnJoinedRoomAgoraRoomView;

            AgoraToUnity = new Dictionary<uint, GameObject>();
            PhotonToUnity = new Dictionary<int, GameObject>();
            AgoraToPhoton = new Hashtable();
        }

        private void SetupButtonListeners()
        {
            ButtonsView.CameraBtn.onClick.AddListener(MyAgoraView.ToggleVideo);
            ButtonsView.Microphone.onClick.AddListener(MyAgoraView.ToggleAudio);
            ButtonsView.Quit.onClick.AddListener(MyAgoraView.Quit);
        }

        private void OnJoinedRoomAgoraRoomView()
        {
            _agoraController = MyAgoraView.Controller;
            _agoraController.SelfUserJoined += AgoraControllerOnSelfUserJoined;
            _agoraController.OtherUserJoined += AgoraControllerOnOtherUserJoined;
        }

        private void Start()
        {   SetupButtonListeners();
            // var currentRoom = PhotonNetwork.CurrentRoom;
            // var players = currentRoom.CustomProperties;
        }

        private void AgoraControllerOnSelfUserJoined(string channel, uint uid, int elapsed, VideoSurface vs)
        {
            // save local data
            _selfAgoraId = uid;
            AgoraToUnity.Add(uid, vs.gameObject);
            var actorNumber = MyPhotonView.Owner.ActorNumber;
            AgoraToPhoton.Add(uid.ToString(), actorNumber);
            
            
            UpdatePlayersObjects();

            // save global data
            var customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            customProperties[actorNumber.ToString()] = uid.ToString(); // convert it to uint
            PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);

            MatchPlayerAndQuads();
        }

        private void AgoraControllerOnOtherUserJoined(uint uid, int elapsed, VideoSurface vs)
        {
            AgoraToUnity.Add(uid, vs.gameObject);
            UpdatePlayersObjects();
            AgoraToPhoton = PhotonNetwork.CurrentRoom.CustomProperties;


            MatchPlayerAndQuads();
        }

        private void UpdatePlayersObjects()
        {
            PhotonToUnity.Clear();
            foreach (var (number, player) in PhotonNetwork.CurrentRoom.Players)
            {
                var playerGo = player.TagObject as GameObject;
                PhotonToUnity.Add(player.ActorNumber, playerGo);
            }
        }

        private void MatchPlayerAndQuads()
        {
            foreach (var (key, value) in AgoraToPhoton)
            {
                if (ReferenceEquals(value, null))
                {
                    continue;
                }

                var uintUid = Convert.ToUInt32(key);
                var goQuad = AgoraToUnity[uintUid];
                var goPlayer = PhotonToUnity[(int) value];

                goQuad.transform.parent = goPlayer.transform.Find($"Header");
                goQuad.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,180f));
                goQuad.transform.localPosition = new Vector3(0f,2f,0f);
                // goQuad.GetComponent<VideoSurface>().SetForUser(uintUid);
            }
        }
    }
}