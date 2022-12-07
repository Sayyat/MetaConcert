using System;
using System.Collections.Generic;
using agora_gaming_rtc;
using Assets.Scripts.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Agora
{
    public class AgoraAndPhotonController
    {
        private readonly AgoraView _agoraView;
        private readonly PhotonView _photonView;

        private AgoraController _agoraController;
        private uint _selfAgoraId { get; set; } = 0;

        public Dictionary<uint, GameObject> AgoraToUnity;
        public Dictionary<int, GameObject> PhotonToUnity;
        public Hashtable PhotonToAgora;
        
        
        public AgoraAndPhotonController(AgoraView agoraView, PhotonView photonView)
        {
            _agoraView = agoraView;
            _photonView = photonView;

            _agoraView.OnJoinedRoom += OnJoinedRoomAgoraRoomView;

            AgoraToUnity = new Dictionary<uint, GameObject>();
            PhotonToUnity = new Dictionary<int, GameObject>();
            PhotonToAgora = new Hashtable();
        }

        private void OnJoinedRoomAgoraRoomView()
        {
            _agoraController = _agoraView.Controller;
            _agoraController.SelfUserJoined += AgoraControllerOnSelfUserJoined;
            _agoraController.OtherUserJoined += AgoraControllerOnOtherUserJoined;
        }

        private void AgoraControllerOnSelfUserJoined(string channel, uint uid, int elapsed, VideoSurface vs)
        {
            // save local data
            _selfAgoraId = uid;
            AgoraToUnity.Add(uid, vs.gameObject);
            var actorNumber = _photonView.Owner.ActorNumber;
            PhotonToAgora.Add (actorNumber.ToString(), uid.ToString());
            
            
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
            
            PhotonToAgora = PhotonNetwork.CurrentRoom.CustomProperties;
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
            foreach (var (photonId, agoraUid) in PhotonToAgora)
            {
                if (ReferenceEquals(photonId, null)||ReferenceEquals(agoraUid, null))
                {
                    continue;
                }

                var uintUid = Convert.ToUInt32(agoraUid);
                var goQuad = AgoraToUnity[uintUid];
                var playerId = Convert.ToInt32(photonId);
                var goPlayer = PhotonToUnity[playerId];

                goQuad.transform.parent = goPlayer.transform;
                goQuad.transform.localPosition = new Vector3(0, 2.5f, 0);
                goQuad.transform.localRotation = Quaternion.Euler(new Vector3(0,0,180f));
            }
        }
    }
}