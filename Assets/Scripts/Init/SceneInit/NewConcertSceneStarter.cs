using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Agora;
using Goods;
using NFT_CUBES;
using Photon.Pun;
using Photon.Realtime;
using PlayerControl;
using Quest;
using StarterAssets;
using UI;
using UnityEngine;
using UnityEngine.Networking;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Init.SceneInit
{
    public class NewConcertSceneStarter : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private GameObject playerFollowCamera;
        [SerializeField] private GameObject userUI;
        [SerializeField] private NftCubesContainer nftCubesContainer;
        [SerializeField] private FindProductsView findProductsView;
        
        [Header("Spawn point settings")] [SerializeField]
        private Vector3 minSpawnPoint;

        [SerializeField] private Vector3 maxSpawnPoint;
        

        private ProductViewPanelController _productViewPanelController;

        private PhotonView _photonView;
        private AgoraView _agoraView;

        private GameObject _photonPlayer;
        private Collector _collector;
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
            findProductsView.UpdateProductsViewPanel(_productViewPanelController);
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
            nftCubesContainer.SetNftButton(_userUIView.NftButton);
            
            //Set starter asset to mobile control 
            var starterAssetsInputs = _photonPlayer.GetComponent<StarterAssetsInputs>();
            _userUIMobile.starterAssetsInputs = starterAssetsInputs;
            
            starterAssetsInputs.TextAlt = _userUIView.TextAlt;

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

            _collector = _photonPlayer.GetComponent<Collector>();
            _collector.CoinGrabbed += CollectorOnCoinGrabbed;
        }

        private void CollectorOnCoinGrabbed(int coinSum, int valueSum)
        {
            _userUIView.CoinProgress.Progress = coinSum;
            if (coinSum == 10)
            {
                var nick = _photonView.Owner.NickName;
                StartCoroutine(Upload(nick));
            }
        }

        private void InstantiateLifts()
        {
            var positions = new List<Vector3>()
            {
                
                new Vector3(28.47f,14.09f,147.28f),
                new Vector3(28.47f,14.09f,132.37f),
                new Vector3(28.47f,14.09f,7.47f),
                new Vector3(28.47f,14.09f,-7.41f),
                new Vector3(28.47f,14.09f,-132.37f),
                new Vector3(28.47f,14.09f,-147.28f),
                new Vector3(-28.5f,14.09f,147.28f),
                new Vector3(-28.5f,14.09f,132.37f),
                new Vector3(-28.5f,14.09f,7.47f),
                new Vector3(-28.5f,14.09f,-7.41f),
                new Vector3(-28.5f,14.09f,-132.37f),
                new Vector3(-28.5f,14.09f,-147.28f),
               
            };

            for (int i = 0; i < positions.Count; i++)
            {
                var yRot = i < 6 ? 0f : 180f;
                var lift = PhotonNetwork.InstantiateRoomObject("Lift", positions[i], Quaternion.Euler(-90f, yRot, 0f));
                Debug.Log($"Instantiated lift: {i}");
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


        private IEnumerator Upload(string nick)
        {
            
            var form = new WWWForm();
            form.AddField("nick", nick);
            form.AddField("time", DateTime.Now.ToString(CultureInfo.CurrentCulture));

            using var www = UnityWebRequest.Post("https://agora-token-generator-beryl.vercel.app/api/upload", form);
            yield return www.SendWebRequest();

            Debug.Log(www.result != UnityWebRequest.Result.Success ? www.error : "Form upload complete!");
        }


        private void OnDestroy()
        {
            _userButtonsController.RemoveAllListeners();
        }
    }
}