using System;
using System.Collections;
using System.Collections.Generic;
using Agora;
using Assets.Scripts.UI;
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


    private GameObject _photonPlayer;
    private PhotonView _photonView;
    private UserUIView _userUIView;
    private UICanvasControllerInput _userUIMobile;
    private UserButtonsView _userUIDesktop;

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

        Instantiate(mainCamera);
        Instantiate(playerFollowcamera);
        _userUIView = Instantiate(UserUI).GetComponent<UserUIView>();
    }

    private void Start()
    {
        _photonPlayer = PhotonNetwork.Instantiate("PlayerTemplate", Vector3.up, Quaternion.identity);
        _photonView = _photonPlayer.GetComponent<PhotonView>();
        _userUIMobile = _userUIView.MobileInput;
        _userUIDesktop = _userUIView.UserButtonsView;

        var starterAssetsInputs = _photonPlayer.GetComponent<StarterAssetsInputs>();
        _userUIMobile.starterAssetsInputs = starterAssetsInputs;

        ConstructAgora();

        _scene.StartScene();


        
        // add my photon id to current room props
        var myId = _photonView.Owner.ActorNumber.ToString();
        var customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        customProperties.Add(myId, null);
        PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);
        
       var t = gameObject.AddComponent<AgoraAndPhotonController>();
       
        _userUIDesktop.onCamera.onClick.AddListener(t._agoraView.JoinVideo);
    }

    private void ConstructAgora()
    {
    }

    public enum Scenes
    {
        Concert,
        Controller
    }
}