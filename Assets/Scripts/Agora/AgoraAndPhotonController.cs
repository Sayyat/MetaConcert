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
        private uint _selfAgoraId;
        private int _selfPhotonId;

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
            //subscribe on quit
            _agoraController.SelfUserLeave += RemoveDataFromRoom;

            // save local data
            _selfAgoraId = uid;
            _agoraVideoObjects.Add(uid, vs.gameObject);

            var actorNumber = _photonView.Owner.ActorNumber;
            _selfPhotonId = actorNumber;

            _photonIdBindAgoraUid.Add(_selfPhotonId.ToString(), _selfAgoraId.ToString());

            UpdatePhotonObjects();
            AddIdDataInRoom();
            MatchPlayerAndQuads();
        }

        private void AgoraControllerOnOtherUserJoined(uint uid, int elapsed, VideoSurface vs)
        {
            _agoraVideoObjects.Add(uid, vs.gameObject);
            UpdatePhotonObjects();

            _photonIdBindAgoraUid = PhotonNetwork.CurrentRoom.CustomProperties;
            MatchPlayerAndQuads();
        }

        private void UpdatePhotonObjects()
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
                if (ReferenceEquals(photonId, null) || ReferenceEquals(agoraUid, null))
                {
                    continue;
                }

                var uintUid = Convert.ToUInt32(agoraUid);
                var goQuad = _agoraVideoObjects[uintUid];
                var playerId = Convert.ToInt32(photonId);
                var goPlayer = _photonPlayerObjects[playerId];

                goQuad.transform.parent = goPlayer.transform;
                goQuad.transform.localPosition = new Vector3(0, 2.5f, 0);
                goQuad.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180f));
            }
        }

        private void AddIdDataInRoom()
        {
            var customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            // if (customProperties.ContainsKey(keyId))

            customProperties[_selfPhotonId.ToString()] = _selfAgoraId.ToString(); // convert it to uint

            PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);
        }

        private void RemoveDataFromRoom()
        {
            var hashtable = new Hashtable();
            var customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            var keyId = _selfPhotonId.ToString();
            
            foreach (var (key, value) in customProperties)
            {
                if (key.ToString() == keyId)
                {
                    continue;
                }

                hashtable.Add(key, value);
            }

            // if (customProperties != null && customProperties.ContainsKey(keyId))
            // {
                // customProperties.Remove(customProperties[keyId]);
                PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
            // }
            // else
            // {
            //     Debug.LogError("Has not or not valid key in this room");
            // }
        }
    }
}