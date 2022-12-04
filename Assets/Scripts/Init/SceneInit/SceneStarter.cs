using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;
using Photon.Pun;
using StarterAssets;
using UI;
using Init.SceneInit;
using Photon.Pun;
using UnityEngine;

public class SceneStarter : MonoBehaviourPunCallbacks
{
   [SerializeField] private GameObject initPlayer;
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
      switch (scenes)
      {
         case Scenes.Concert:
            _scene = gameObject.AddComponent<ConcertScene>();
            break;
         case Scenes.Controller:
            _scene = new ControllerScene();
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
      Instantiate(initPlayer);
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

      Instantiate(UserUI);

      ConstructAgora();
      
      _scene.StartScene();
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
