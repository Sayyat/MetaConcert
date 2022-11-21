using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
   [SerializeField] private GameObject initPlayer;
   [SerializeField] private GameObject mainCamera;
   [SerializeField] private GameObject playerFollowcamera;
   [SerializeField] private GameObject UserUI;

   private void Awake()
   {
      Instantiate(initPlayer);
      Instantiate(mainCamera);
      Instantiate(playerFollowcamera);
      Instantiate(UserUI);
   }
}
