using System;
using System.Collections;
using System.Collections.Generic;
using Agora;
using Assets.Scripts.UI;
using Goods;
using Photon.Pun;
using StarterAssets;
using UI;
using Init.SceneInit;
using Photon.Pun;
using UnityEngine;

public class SceneStarter : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject playerFollowcamera;
    [SerializeField] private GameObject UserUI;
    [SerializeField] private ProductViewController productViewController;
    
    private ProductViewPanelController _productViewPanelController;
    

    private PhotonView _photonView;
    private AgoraView _agoraView;
    
    private GameObject _photonPlayer;
    private UserUIView _userUIView;
    private UICanvasControllerInput _userUIMobile;
    private UserButtonsView _userButtonsView;
    private AgoraAndPhotonController _agoraAndPhotonController;
    
    
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
        _userUIView = Instantiate(UserUI).GetComponent<UserUIView>();

        // _userUIView.GoodsViewPanel.Init();
        _productViewPanelController = new ProductViewPanelController(_userUIView.ProductViewPanel);
        productViewController.ProductViewPanelController = _productViewPanelController;
    }

    private void Start()
    {
        _photonPlayer = PhotonNetwork.Instantiate("PlayerTemplate", Vector3.up, Quaternion.identity);
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
        
        var userButtonsController = new UserButtonsController(_userButtonsView, _agoraView);
        userButtonsController.SetupButtons();
        // _agoraAndPhotonController.SetupButtonListeners();
    }

    private void ConstructAgora()
    {
    }

    private enum Scenes
    {
        Concert,
        Controller
    }
}