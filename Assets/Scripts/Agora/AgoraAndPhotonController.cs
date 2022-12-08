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

        private readonly Dictionary<uint, GameObject> _agoraVideoObjects;
        private readonly Dictionary<int, GameObject> _photonPlayerObjects;
        private Hashtable _photonIdBindAgoraUid;
        
        
        public AgoraAndPhotonController(AgoraView agoraView, PhotonView photonView)
        {
            _agoraView = agoraView;
            _photonView = photonView;

            _agoraView.OnJoinedRoom += OnJoinedRoomAgoraRoomView;

            _agoraVideoObjects = new Dictionary<uint, GameObject>();
            _photonPlayerObjects = new Dictionary<int, GameObject>();
            _photonIdBindAgoraUid = new Hashtable();
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
            _agoraVideoObjects.Add(uid, vs.gameObject);
            var actorNumber = _photonView.Owner.ActorNumber;
            _photonIdBindAgoraUid.Add (actorNumber.ToString(), uid.ToString());
            
            
            UpdatePlayersObjects();

            // save global data
            var customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            customProperties[actorNumber.ToString()] = uid.ToString(); // convert it to uint
            PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);
            
            
            MatchPlayerAndQuads();
        }

        private void AgoraControllerOnOtherUserJoined(uint uid, int elapsed, VideoSurface vs)
        {
            _agoraVideoObjects.Add(uid, vs.gameObject);
            UpdatePlayersObjects();
            
            _photonIdBindAgoraUid = PhotonNetwork.CurrentRoom.CustomProperties;
            MatchPlayerAndQuads();
        }

        private void UpdatePlayersObjects()
        {
            _photonPlayerObjects.Clear();
            foreach (var (number, player) in PhotonNetwork.CurrentRoom.Players)
            {
                var playerGo = player.TagObject as GameObject;
                _photonPlayerObjects.Add(player.ActorNumber, playerGo);
            }
        }

        private void MatchPlayerAndQuads()
        {
            foreach (var (photonId, agoraUid) in _photonIdBindAgoraUid)
            {
                if (ReferenceEquals(photonId, null)||ReferenceEquals(agoraUid, null))
                {
                    continue;
                }

                var uintUid = Convert.ToUInt32(agoraUid);
                var goQuad = _agoraVideoObjects[uintUid];
                var playerId = Convert.ToInt32(photonId);
                var goPlayer = _photonPlayerObjects[playerId];

                goQuad.transform.parent = goPlayer.transform;
                goQuad.transform.localPosition = new Vector3(0, 2.5f, 0);
                goQuad.transform.localRotation = Quaternion.Euler(new Vector3(0,0,180f));
            }
        }
    }
}