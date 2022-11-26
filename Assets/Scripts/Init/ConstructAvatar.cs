using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Init
{
    [RequireComponent(typeof(Animator))]
    public class ConstructAvatar : MonoBehaviourPun, IPunInstantiateMagicCallback, IInRoomCallbacks
    {
        private Animator _animator;
        private Avatar _avatarScheme;
        private string _currentAvatarUrl;
        private AvatarCashes _avatarCashes;

        private bool _canLoadAvatar = true;

        private string _actorNumber;
        // public string CurrentAvatarUrl
        // {
        //     get => _currentAvatarUrl;
        //     set
        //     {
        //         _currentAvatarUrl = value;
        //         LoadAvatar();
        //     }
        // }


        private void Awake()
        {
            //Added this class to callback observed
            PhotonNetwork.NetworkingClient.AddCallbackTarget(this);

            //Make a normal receipt of the cache link
            _avatarCashes = GameObject.Find("AvatarCashes").GetComponent<AvatarCashes>();

            if (photonView.IsMine)
            {
                _currentAvatarUrl = _avatarCashes.SelectedAvatarUrl;

                Debug.Log($"Getting my avatar from cash: {_currentAvatarUrl}");
            }

            _animator = GetComponent<Animator>();
        }


        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            if (_actorNumber != null)
                return;

            _actorNumber = info.Sender.ActorNumber.ToString();

            var hashTable = PhotonNetwork.CurrentRoom.CustomProperties;

            if (hashTable.ContainsKey(_actorNumber))
            {
                hashTable[_actorNumber] = _currentAvatarUrl;
            }
            else
            {
                hashTable.Add(_actorNumber, _currentAvatarUrl);
            }

            Debug.Log("Add new Player in Room property");

            if (_currentAvatarUrl != null)
            {
                PhotonNetwork.CurrentRoom.SetCustomProperties(hashTable);
            }
        }

        private void LoadAvatar()
        {
            _canLoadAvatar = false;

            var properties = PhotonNetwork.CurrentRoom.CustomProperties;
            var urlAvatar = properties[_actorNumber] as string;

            var go = Instantiate(_avatarCashes.GetAvatar(urlAvatar));
            Construct(go);
        }

        private void Construct(GameObject playerTemplate)
        {
            _avatarScheme = playerTemplate.GetComponent<Animator>().avatar;

            // todo add smart method to grab children
            var mesh = playerTemplate.transform.GetChild(0);
            var armature = playerTemplate.transform.GetChild(1);


            mesh.gameObject.layer = LayerMask.NameToLayer("Player");
            armature.gameObject.layer = LayerMask.NameToLayer("Player");

            mesh.transform.parent = transform;
            armature.transform.parent = transform;


            mesh.transform.position = Vector3.zero;
            armature.transform.position = Vector3.zero;

            Destroy(playerTemplate);

            _animator.avatar = _avatarScheme;
        }

        public void OnPlayerEnteredRoom(Player newPlayer)
        {
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (!PhotonNetwork.IsMasterClient || !photonView.IsMine) return;

            var hashTable = PhotonNetwork.CurrentRoom.CustomProperties;
            hashTable.Remove(otherPlayer.ActorNumber.ToString());

            Debug.Log("Remove player from Room property");

            PhotonNetwork.CurrentRoom.SetCustomProperties(hashTable);
        }

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if (propertiesThatChanged.ContainsKey(_actorNumber) && _canLoadAvatar)
            {
                LoadAvatar();
            }
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
        }

        public void OnMasterClientSwitched(Player newMasterClient)
        {
        }


        private void OnDisable()
        {
            
            PhotonNetwork.NetworkingClient.RemoveCallbackTarget(this);
        }
    }
}