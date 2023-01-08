using System;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Lift
{
    public class LiftController : MonoBehaviour
    {
        [SerializeField] private Transform platform;

        [Header("Spots were lift can stop")] [SerializeField]
        private List<Transform> liftStopSpots;

        [Header("Buttons to call lift")] [SerializeField]
        private List<Button3d> liftCallButtons;

        [Header("Buttons inside the lift")] [SerializeField]
        private List<Button3d> floorButtons;

        private List<Transform> _liftStopSpotsGlobal;
        
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            PhotonNetwork.RegisterPhotonView(_photonView);
            _liftStopSpotsGlobal = new List<Transform>();

            foreach (var liftStopSpot in liftStopSpots)
            {
                _liftStopSpotsGlobal.Add(liftStopSpot.transform);
            }
        }

        private void Start()
        {
            for (int i = 0; i < liftCallButtons.Count; i++)
            {
                liftCallButtons[i].ButtonClicked += OnButtonClicked;
                floorButtons[i].ButtonClicked += OnButtonClicked;
            }


            // Debug.Log(l);
        }

        private void OnDestroy()
        {
            liftCallButtons.ForEach(button => { button.ButtonClicked -= OnButtonClicked; });
            floorButtons.ForEach(button => { button.ButtonClicked -= OnButtonClicked; });
        }

        private void OnButtonClicked(int floor)
        {
            _photonView.RPC(nameof(RPC_MovePlatform), RpcTarget.All, floor);
        }

        [PunRPC]
        private void RPC_MovePlatform(int floor)
        {
            var destination = _liftStopSpotsGlobal[floor - 1].position;
            platform.DOMove(destination, 2f);
        }
    }
}