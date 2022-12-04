using System;
using System.Collections.Generic;
using agora_gaming_rtc;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Agora
{
    public class AgoraAndPhotonController : MonoBehaviourPunCallbacks
    {
        public AgoraView _agoraView { get; set; }
        private AgoraController _agoraController;


        private bool _inAgoraRoom { get; set; } = false;
        private bool _inPhotonRoom { get; set; } = false;
        private uint _selfAgoraId { get; set; } = 0;

        public Dictionary<uint, GameObject> _agoraToUnity;
        public Dictionary<int, GameObject> _photonToUnity;
        public Hashtable _agoraToPhoton;

        private void Awake()
        {
            _agoraView = gameObject.GetComponent<AgoraView>() ?? gameObject.AddComponent<AgoraView>();

            _agoraView.IsJoin += OnJoinAgoraView;
        }

        private void OnJoinAgoraView()
        {
            _agoraController = _agoraView._controller;
            _agoraController.SelfUserJoined += AgoraControllerOnSelfUserJoined;
            _agoraController.OtherUserJoined += AgoraControllerOnOtherUserJoined;
        }

        private void Start()
        {
            // var currentRoom = PhotonNetwork.CurrentRoom;
            // var players = currentRoom.CustomProperties;
        }

        private void AgoraControllerOnSelfUserJoined(string channel, uint uid, int elapsed, GameObject go)
        {
            // save local data
            _selfAgoraId = uid;
            _agoraToUnity.Add(uid, go);
            var actorNumber = photonView.Owner.ActorNumber;
            _agoraToPhoton.Add(uid.ToString(), actorNumber);
            UpdatePlayersObjects();

            // save global data
            var myId = photonView.Owner.ActorNumber.ToString();
            var customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            customProperties[myId] = uid.ToString(); // convert it to uint
            PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);

            MatchPlayerAndQuads();
        }

        private void AgoraControllerOnOtherUserJoined(uint uid, int elapsed, GameObject go)
        {
            _agoraToUnity.Add(uid, go);
            UpdatePlayersObjects();
            _agoraToPhoton = PhotonNetwork.CurrentRoom.CustomProperties;


            MatchPlayerAndQuads();
        }

        private void UpdatePlayersObjects()
        {
            _photonToUnity.Clear();
            foreach (var (number, player) in PhotonNetwork.CurrentRoom.Players)
            {
                var playerGo = player.TagObject as GameObject;
                _photonToUnity.Add(player.ActorNumber, playerGo);
            }
        }

        private void MatchPlayerAndQuads()
        {
            foreach (var (key, value) in _agoraToPhoton)
            {
                if (ReferenceEquals(value, null))
                {
                    continue;
                }

                var uintUid = (uint) key;
                var goQuad = _agoraToUnity[uintUid];
                var goPlayer = _photonToUnity[(int) value];

                goQuad.transform.parent = goPlayer.transform.Find($"Header/Video_{key}");
                goQuad.GetComponent<VideoSurface>().SetForUser(uintUid);
            }
        }
    }
}