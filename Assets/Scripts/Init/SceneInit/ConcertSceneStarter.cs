using System;
using System.Collections.Generic;
using System.Numerics;
using Agora;
using ExitGames.Client.Photon;
using Goods;
using Photon.Pun;
using Photon.Realtime;
using PlayerControl;
using StarterAssets;
using UI;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Init.SceneInit
{
    public class ConcertSceneStarter : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private GameObject playerFollowCamera;
        [SerializeField] private GameObject userUI;
        [SerializeField] private ProductViewController productViewController;

        [SerializeField] private Transform liftsParent;

        [Header("Spawn point settings")] [SerializeField]
        private Vector3 minSpawnPoint;

        [SerializeField] private Vector3 maxSpawnPoint;


        private ProductViewPanelController _productViewPanelController;


        private PhotonView _photonView;
        private AgoraView _agoraView;

        private GameObject _photonPlayer;
        private UserUIView _userUIView;
        private UICanvasControllerInput _userUIMobile;
        private UserButtonsView _userButtonsView;
        private AgoraAndPhotonController _agoraAndPhotonController;
        private UserButtonsController _userButtonsController;

        private void Awake()
        {
            _agoraView = GetComponent<AgoraView>();

            Instantiate(mainCamera);
            Instantiate(playerFollowCamera);
            _userUIView = Instantiate(userUI).GetComponent<UserUIView>();

            // _userUIView.GoodsViewPanel.Init();
            _productViewPanelController = new ProductViewPanelController(_userUIView.ProductViewPanel);
            productViewController.ProductViewPanelController = _productViewPanelController;
            _userUIView.NamePickGui.autoStart = true;
        }

        private void Start()
        {
            // calculate random position
            var x = Random.Range(Math.Min(minSpawnPoint.x, maxSpawnPoint.x),
                Math.Max(minSpawnPoint.x, maxSpawnPoint.x));
            var y = Random.Range(Math.Min(minSpawnPoint.y, maxSpawnPoint.y),
                Math.Max(minSpawnPoint.y, maxSpawnPoint.y));
            var z = Random.Range(Math.Min(minSpawnPoint.z, maxSpawnPoint.z),
                Math.Max(minSpawnPoint.z, maxSpawnPoint.z));

            _photonPlayer = PhotonNetwork.Instantiate("PlayerTemplate", new Vector3(x, y, z), Quaternion.identity);
            _photonView = _photonPlayer.GetComponent<PhotonView>();
            _agoraAndPhotonController = new AgoraAndPhotonController(_agoraView, _photonView);

            _userUIMobile = _userUIView.MobileInput;
            _userButtonsView = _userUIView.UserButtonsView;

            //Set starter asset to mobile control 
            var starterAssetsInputs = _photonPlayer.GetComponent<StarterAssetsInputs>();
            _userUIMobile.starterAssetsInputs = starterAssetsInputs;

            ConstructAgora();

            // add my photon id to current room props

            var myId = _photonView.Owner.ActorNumber.ToString();
            var customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            customProperties.Add(myId, null);
            PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);

            var animationControl = _photonPlayer.GetComponent<AnimationControl>();

            _userButtonsController =
                new UserButtonsController(_userButtonsView, _agoraView, _photonView, animationControl);
            _userButtonsController.SetupButtons();

            var count = PhotonNetwork.CurrentRoom.PlayerCount;
            if (count == 1)
            {
                InstantiateLifts();
            }
        }

        private void InstantiateLifts()
        {
            var positions = new List<Vector3>()
            {
                new Vector3(31f, 0f, -16.6f),
                new Vector3(31f, 0f, -31.38f),
                new Vector3(31f, 0f, -156.6f),
                new Vector3(31f, 0f, -171.08f),
                new Vector3(31f, 0f, -296.07f),
                new Vector3(31f, 0f, -310.85f),
                new Vector3(-25.95f, 0f, -16.6f),
                new Vector3(-25.95f, 0f, -31.38f),
                new Vector3(-25.95f, 0f, -156.6f),
                new Vector3(-25.95f, 0f, -171.08f),
                new Vector3(-25.95f, 0f, -296.07f),
                new Vector3(-25.95f, 0f, -310.85f),
            };

            for (int i = 0; i < positions.Count; i++)
            {
                var yRot = i < 6 ? 0f : 180f;
                var lift = PhotonNetwork.Instantiate("Lift", positions[i], Quaternion.Euler(-90f, 0f, 0f), 0);
            }
        }

        private void ConstructAgora()
        {
        }


        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            var state = Convert.ToBoolean(changedProps["IsVideoOn"]);
            _agoraAndPhotonController.ToggleVideoQuad(targetPlayer.ActorNumber, state);
        }

        private void OnDestroy()
        {
            _userButtonsController.RemoveAllListeners();
        }
    }
}