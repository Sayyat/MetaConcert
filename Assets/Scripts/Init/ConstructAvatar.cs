using System;
using AvatarLoader;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Init
{
    [RequireComponent(typeof(Animator))]
    public class ConstructAvatar : MonoBehaviourPun, IPunInstantiateMagicCallback, IInRoomCallbacks, IOnEventCallback
    {
        private Animator _animator;
        private Avatar _avatarScheme;
        private string _currentAvatarUrl;
        private AvatarCashes _avatarCashes;

        private bool _isDownLoaded = false;

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

            // get AvatarCache object link from DontDestroy
            _avatarCashes = GameObject.Find("AvatarCashes").GetComponent<AvatarCashes>();

            // if its me, i get my avatarUrl from AvatarCache
            if (photonView.IsMine)
            {
                _currentAvatarUrl = _avatarCashes.SelectedAvatarUrl;
                Debug.Log($"Getting my avatar from cash: {_currentAvatarUrl}");
            }

            // set my animator
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            if (!photonView.IsMine) return;

            if (Input.GetKeyDown(KeyCode.P))
            {
                var players = PhotonNetwork.CurrentRoom.Players;
                Debug.Log("Players List");

                foreach (var player in players)
                {
                    Debug.Log(
                        $"<Color=Red>{player.Value}</Color>: <Color=Green>{player.Value.CustomProperties}</Color> ");
                }
            }
        }

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            info.Sender.TagObject = gameObject;

            if (!photonView.IsMine) return;
            // do these only if it is me

            // // save my avatarUrl to my own CustomProperties
            // var hashTable = photonView.Owner.CustomProperties;
            // if (hashTable.ContainsKey("avatarUrl"))
            // {
            //     hashTable["avatarUrl"] = _currentAvatarUrl;
            // }
            // else
            // {
            //     hashTable.Add("avatarUrl", _currentAvatarUrl);
            // }
            //
            // photonView.Owner.SetCustomProperties(hashTable);
            photonView.RPC(nameof(LoadAvatar), RpcTarget.All, _currentAvatarUrl);
            Debug.Log($"<Color=Red>{info.photonView.Owner.NickName} is instantiated</Color>");
        }
        
        
        [PunRPC]
        private void LoadAvatar(string url)
        {
            Debug.Log($" Pre Instantiate avatar ");
            // construct my avatar from my avatarUrl
            var go = GameObject.Instantiate(_avatarCashes.GetAvatar(url));

            Debug.Log($"Instantiate avatar{go}");

            Construct(go);
        }

        private void Construct(GameObject playerTemplate)
        {
            // skip if this clone already downloaded its avatar
            if (_isDownLoaded) return;

            _avatarScheme = playerTemplate.GetComponent<Animator>().avatar;

            Debug.Log($"Get Animator");
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


            Debug.Log($"Destroy GO");

            _animator.avatar = _avatarScheme;
            _isDownLoaded = true;
        }


        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            photonView.RPC(nameof(LoadAvatar), RpcTarget.Others, _currentAvatarUrl);
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
        }

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            // var props = photonView.Owner.CustomProperties;
            // if (!props.ContainsKey("avatarUrl")) return;
            //
            // var url = Convert.ToString(props["avatarUrl"]);
            // LoadAvatar(url);
        }

        public void OnMasterClientSwitched(Player newMasterClient)
        {
        }


        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.RemoveCallbackTarget(this);
        }

        public void OnEvent(EventData photonEvent)
        {
        }
    }
}