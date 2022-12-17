using System;
using Agora;
using Assets.Scripts.UI;
using ExitGames.Client.Photon;
using Goods;
using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Init.SceneInit
{
    public class SceneStarter : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private GameObject playerFollowcamera;
        [SerializeField] private GameObject userUI;
        [SerializeField] private ProductViewController productViewController;

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


        [SerializeField] private Scenes scenes = Scenes.Concert;

        private IScene _scene;

        private void Awake()
        {
            _scene = scenes switch
            {
                Scenes.Concert => gameObject.AddComponent<ConcertScene>(),
                Scenes.Controller => new ControllerScene(),
                _ => throw new ArgumentOutOfRangeException()
            };

            _agoraView = GetComponent<AgoraView>();

            Instantiate(mainCamera);
            Instantiate(playerFollowcamera);
            _userUIView = Instantiate(userUI).GetComponent<UserUIView>();

            // _userUIView.GoodsViewPanel.Init();
            _productViewPanelController = new ProductViewPanelController(_userUIView.ProductViewPanel);
            productViewController.ProductViewPanelController = _productViewPanelController;
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

            _scene.StartScene();

            // add my photon id to current room props

            var myId = _photonView.Owner.ActorNumber.ToString();
            var customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            customProperties.Add(myId, null);
            PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);

            _userButtonsController = new UserButtonsController(_userButtonsView, _agoraView, _photonView);
            _userButtonsController.SetupButtons();
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


        private enum Scenes
        {
            Concert,
            Controller
        }
    }
}