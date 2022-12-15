using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lift
{
    public class LiftController : MonoBehaviour
    {
        [SerializeField] private PlatformMove platformMove;
        [SerializeField] private PlatformTrigger platformTrigger;

        [Header("Spots were lift can stop")] [SerializeField]
        private List<Transform> liftStopSpots;

        [Header("Buttons to call lift")] [SerializeField]
        private List<Button3d> liftCallButtons;

        [Header("Buttons inside the lift")] [SerializeField]
        private List<Button3d> floorButtons;

        private List<Transform> _liftStopSpotsGlobal;
        private LiftStates _liftStates;

        private List<Transform> PlayerTransforms => platformTrigger.PlayersTransform;
        
        private void Awake()
        {
            _liftStates = new LiftStates();
            _liftStopSpotsGlobal = new List<Transform>();

            foreach (var liftStopSpot in liftStopSpots)
            {
                _liftStopSpotsGlobal.Add(liftStopSpot.transform);
            }
        }

        private void Start()
        {
            string l = "<Color=Green>\n";

            for (int i = 0; i < liftCallButtons.Count; i++)
            {
                l += liftCallButtons[i].ToString() + "\n";
                liftCallButtons[i].ButtonClicked += OnButtonClicked;
                floorButtons[i].ButtonClicked += OnButtonClicked;
            }

            l += "</Color>";

            // Debug.Log(l);
        }

        private void OnDestroy()
        {
            liftCallButtons.ForEach(button => { button.ButtonClicked -= OnButtonClicked; });
            floorButtons.ForEach(button => { button.ButtonClicked -= OnButtonClicked; });
        }

        private void OnButtonClicked(int floor)
        {
            platformMove.enabled = true;
            platformMove.players = PlayerTransforms;
            platformMove.Destination = _liftStopSpotsGlobal[floor - 1].position;
            _liftStates.IsMoving = true;
        }
    }
}