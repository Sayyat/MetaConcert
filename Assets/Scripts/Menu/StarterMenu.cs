using System;
using System.Collections.Generic;
using AvatarLoader;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

namespace Assets.Scripts
{
#pragma warning disable 649

    /// <summary>
    /// Launch manager. Connect, join a random room or create one if none or all full.
    /// </summary>
    public class StarterMenu : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields

        [Tooltip("The Ui Panel to let the user enter name, connect and play")] [SerializeField]
        private GameObject videoPanel;

        [Tooltip("The maximum number of players per room")] [SerializeField]
        private byte maxPlayersPerRoom = 20;


        [Tooltip("The scene name we want to load")] [SerializeField]
        private string desiredScene = "Concert";


        private AvatarCashes _avatarCashes;

        #endregion

        #region Private Fields

        /// <summary>
        /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
        /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
        /// Typically this is used for the OnConnectedToMaster() callback.
        /// </summary>
        bool isConnecting;

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "1";

        #endregion

        #region MonoBehaviour CallBacks

        [SerializeField] private AvatarRenderView avatarRenderView;

        private List<string> urls = new List<string>()
        {
            // "https://api.readyplayer.me/v1/avatars/635e103a1260644e7e39a393.glb",
            // "https://api.readyplayer.me/v1/avatars/635cfc42124f746eb3af6476.glb",
            // "https://api.readyplayer.me/v1/avatars/635e12af124f746eb3b0969b.glb",
            // "https://api.readyplayer.me/v1/avatars/635e13561260644e7e39a53b.glb"
            "https://api.readyplayer.me/v1/avatars/6360d011fff3a4d4797b7cf1.glb",
            "https://api.readyplayer.me/v1/avatars/637770d9152ef07e24279cdf.glb",
            "https://api.readyplayer.me/v1/avatars/63775fb2152ef07e24278a03.glb",
            "https://api.readyplayer.me/v1/avatars/637871d1a9869f44e5e7a2ce.glb"
        };

        private AvatarRenderController _avatarRenderController;


        private void Start()
        {
            Debug.Log("Try to find existing DataPlayerAvatar object");
            var AvatarCashes = GameObject.Find("AvatarCashes");

            if (AvatarCashes == null)
            {
                AvatarCashes = new GameObject("AvatarCashes");
            }

            _avatarCashes = AvatarCashes.GetComponent<AvatarCashes>();

            if (_avatarCashes == null)
            {
                _avatarCashes = AvatarCashes.AddComponent<AvatarCashes>();
            }

            _avatarRenderController = new AvatarRenderController(avatarRenderView, urls, _avatarCashes, videoPanel);

            _avatarCashes.PreloadAvatars(urls);
        }

        private void OnDestroy()
        {
            _avatarRenderController.Dispose();
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Start the connection process. 
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect()
        {
            // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
            isConnecting = true;



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
                PhotonNetwork.GameVersion = this.gameVersion;
            }
        }

        /// <summary>
        /// Logs the feedback in the UI view for the player, as opposed to inside the Unity Editor for the developer.
        /// </summary>
        /// <param name="message">Message.</param>
        void LogFeedback(string message)
        {
            Debug.Log($"LOG FEEDBACK: {message}");
        }

        #endregion


        #region MonoBehaviourPunCallbacks CallBacks

        // below, we implement some callbacks of PUN
        // you can find PUN's callbacks in the class MonoBehaviourPunCallbacks


        /// <summary>
        /// Called after the connection to the master is established and authenticated
        /// </summary>
        public override void OnConnectedToMaster()
        {
            // we don't want to do anything if we are not attempting to join a room. 
            // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
            // we don't want to do anything.
            if (isConnecting)
            {
                LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room");
                Debug.Log(
                    "PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");

                // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
                PhotonNetwork.JoinRandomRoom();
            }
        }

        /// <summary>
        /// Called when a JoinRandom() call failed. The parameter provides ErrorCode and message.
        /// </summary>
        /// <remarks>
        /// Most likely all rooms are full or no rooms are available. <br/>
        /// </remarks>
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room");
            Debug.Log(
                "PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = this.maxPlayersPerRoom});
        }


        /// <summary>
        /// Called after disconnecting from the Photon server.
        /// </summary>
        public override void OnDisconnected(DisconnectCause cause)
        {
            LogFeedback("<Color=Red>OnDisconnected</Color> " + cause);
            Debug.LogError("PUN Basics Tutorial/Launcher:Disconnected");


            isConnecting = false;
        }

        /// <summary>
        /// Called when entering a room (by creating or joining it). Called on all clients (including the Master Client).
        /// </summary>
        /// <remarks>
        /// This method is commonly used to instantiate player characters.
        /// If a match has to be started "actively", you can call an [PunRPC](@ref PhotonView.RPC) triggered by a user's button-press or a timer.
        ///
        /// When this is called, you can usually already access the existing players in the room via PhotonNetwork.PlayerList.
        /// Also, all custom properties should be already available as Room.customProperties. Check Room..PlayerCount to find out if
        /// enough players are in the room to start playing.
        /// </remarks>
        public override void OnJoinedRoom()
        {
            LogFeedback(
                "<Color=Green>OnJoinedRoom</Color> with " + PhotonNetwork.CurrentRoom.PlayerCount + " Player(s)");
            Debug.Log(
                "PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running.");

            // #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.AutomaticallySyncScene to sync our instance scene.

            Debug.Log($"We load the '{desiredScene}' ");

            // #Critical
            // Load the Room Level. 
            PhotonNetwork.LoadLevel(desiredScene);
        }

        #endregion
    }
}