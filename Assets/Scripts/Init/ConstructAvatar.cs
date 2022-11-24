﻿using System;
using AvatarLoader;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using ReadyPlayerMe;
using TMPro;
using UnityEngine;

namespace Init
{
    [RequireComponent(typeof(Animator))]
    public class ConstructAvatar : MonoBehaviourPun, IPunInstantiateMagicCallback, IInRoomCallbacks
    {
        [SerializeField] private GameObject defaultAvatar;
        [SerializeField] private TextMeshPro progress;

        private Animator _animator;
        private Avatar _avatarScheme;
        private ReadyPlayerMe.AvatarLoader _avatarLoader;
        private string _currentAvatarUrl;
        private AvatarCashes _avatarCashes;
        private bool _canLoadAvatar = false;

        public string CurrentAvatarUrl
        {
            get => _currentAvatarUrl;
            set
            {
                _currentAvatarUrl = value;
                LoadAvatar();
            }
        }


        private void Awake()
        {
            //Added this class to callback observed
            PhotonNetwork.NetworkingClient.AddCallbackTarget(this);


            _avatarCashes = GameObject.Find("AvatarCashes").GetComponent<AvatarCashes>();
            _currentAvatarUrl = _avatarCashes.SelectedAvatarUrl;

            Debug.Log($"Selected avatarurl: {_currentAvatarUrl}");


            _animator = GetComponent<Animator>();
        }


        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            var avatarUrl = photonView.Owner.CustomProperties.ToString();

            Debug.Log($"Custom properties : {avatarUrl}");
            LoadAvatar();
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                // Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties.Count);
            }
        }

        private void LoadAvatar()
        {
            if (_avatarCashes.HasAvatar3d(_currentAvatarUrl))
            {
                var playerAvatar = _avatarCashes.DataPlayerAvatars[_currentAvatarUrl];
                Debug.Log($"Avatar loaded from cache: {playerAvatar.Avatar3d.Url}");
                Construct(playerAvatar.Avatar3d.Avatar);
                return;
            }

            Debug.Log("Avatar not found in cache, start downloading from readyplayer");

            _avatarLoader = new ReadyPlayerMe.AvatarLoader();

            _avatarLoader.OnCompleted += ConstructOnSuccess;
            _avatarLoader.OnProgressChanged += ProgressChanged;
            _avatarLoader.OnFailed += ConstructOnFailed;


            _avatarLoader.LoadAvatar(_currentAvatarUrl);
        }

        private void ProgressChanged(object sender, ProgressChangeEventArgs e)
        {
            float p = e.Progress * 100;
            progress.text = p + " %";

            if (e.Progress == 1.0f)
            {
                progress.gameObject.SetActive(false);
            }
        }

        public void ConstructOnSuccess(object sender, CompletionEventArgs args)
        {
            // save 3d avatar into cache
            _avatarCashes.AddAvatar3d(args.Url, new AvatarModel()
            {
                Avatar = args.Avatar,
                Metadata = args.Metadata,
                Url = args.Url
            });

            Construct(args.Avatar);
            Debug.Log("Avatar loaded successfully");
        }


        public void ConstructOnFailed(object sender, FailureEventArgs args)
        {
            var avatar3d = new AvatarModel()
            {
                Avatar = defaultAvatar
            };

            var go = Instantiate(defaultAvatar);

            Construct(go);
            Debug.Log("Failed to load avatar. Creating default avatar");
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

        private void OnDestroy()
        {
            _avatarLoader.OnCompleted -= ConstructOnSuccess;
            _avatarLoader.OnProgressChanged -= ProgressChanged;
            _avatarLoader.OnFailed -= ConstructOnFailed;
        }

        public void OnPlayerEnteredRoom(Player newPlayer)
        {
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
        }

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
        }

        public void OnMasterClientSwitched(Player newMasterClient)
        {
        }
    }
}