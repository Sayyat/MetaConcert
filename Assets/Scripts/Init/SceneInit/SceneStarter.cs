using System;
using System.Collections;
using System.Collections.Generic;
using Init.SceneInit;
using Photon.Pun;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
   [SerializeField] private GameObject initPlayer;
   [SerializeField] private GameObject mainCamera;
   [SerializeField] private GameObject playerFollowcamera;
   [SerializeField] private GameObject UserUI;
   
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
