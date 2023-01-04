using System.Collections.Generic;
using AvatarLoader;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Menu
{
#pragma warning disable 649

    /// <summary>
    /// Launch manager. Connect, join a random room or create one if none or all full.
    /// </summary>
    public class StarterMenu : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields

        [Tooltip("Panel controller")] [SerializeField]
        private PanelControl panelController;


        [Tooltip("The maximum number of players per room")] [SerializeField]
        private byte maxPlayersPerRoom = 20;


        [Tooltip("The scene name we want to load")] [SerializeField]
        private Scenes desiredScene = Scenes.Concert;

        [SerializeField] private AvatarRenderView avatarRenderView;

        #endregion

        private enum Scenes
        {
            Concert,
            Control,
            NewConcert
        }

        #region Private Fields

        private bool _isConnecting;

        private const string GameVersion = "1";


        private readonly List<string> _urls = new List<string>()
        {
            "https://api.readyplayer.me/v1/avatars/637770d9152ef07e24279cdf.glb",
            "https://api.readyplayer.me/v1/avatars/6360d011fff3a4d4797b7cf1.glb",
            "https://api.readyplayer.me/v1/avatars/63775fb2152ef07e24278a03.glb",
            "https://api.readyplayer.me/v1/avatars/63777cba152ef07e2427ac52.glb",
            "https://api.readyplayer.me/v1/avatars/637871d1a9869f44e5e7a2ce.glb"
        };

        private AvatarCashes _avatarCashes;
        private MainLoadAvatars _mainLoadAvatars;
        private AvatarRenderController _avatarRenderController;

        #endregion

        private void Start()
        {
            
            Application.targetFrameRate = -1; // max available fps or 60 
            Debug.Log("Try to find existing DataPlayerAvatar object");
            var avatarCashes = GameObject.Find("AvatarCashes");

            if (avatarCashes == null)
            {
                avatarCashes = new GameObject("AvatarCashes");
                Debug.LogError("Dont have Avatar cashes. New Cash");
            }

            _avatarCashes = avatarCashes.GetComponent<AvatarCashes>();

            if (_avatarCashes == null)
            {
                _avatarCashes = avatarCashes.AddComponent<AvatarCashes>();
            }

            _avatarRenderController =
                new AvatarRenderController(avatarRenderView, _urls, _avatarCashes);
            _mainLoadAvatars =
                new MainLoadAvatars(_avatarCashes, _avatarRenderController);

            var urlSet = new HashSet<string>(_urls);
            StartCoroutine(_mainLoadAvatars.LoadAvatars(urlSet));
        }

        private void OnDestroy()
        {
            _avatarRenderController.Dispose();
            StopAllCoroutines();
        }

        #region Public Methods

        public void Connect()
        {
            // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
            _isConnecting = true;


            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
            if (PhotonNetwork.IsConnected)
            {
                LogFeedback("Joining Room...");
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                LogFeedback("Connecting...");

                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = GameVersion;
            }
        }

        private void LogFeedback(string message)
        {
            Debug.Log($"LOG FEEDBACK: {message}");
        }

        #endregion

        #region MonoBehaviourPunCallbacks CallBacks

        public override void OnConnectedToMaster()
        {
            // we don't want to do anything if we are not attempting to join a room. 
            // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
            // we don't want to do anything.
            if (_isConnecting)
            {
                LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room");
                Debug.Log(
                    "PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");

                // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room");
            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom });
        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            LogFeedback("<Color=Red>OnDisconnected</Color> " + cause);


            _isConnecting = false;
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(desiredScene.ToString());
        }

        #endregion
    }
}