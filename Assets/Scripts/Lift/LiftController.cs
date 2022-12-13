using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lift
{
    public class LiftController : MonoBehaviour
    {
        [SerializeField] private PlatformController platform;

        [Header("Spots were lift can stop")] [SerializeField]
        private List<Transform> liftStopSpots;

        [Header("Buttons to call lift")] [SerializeField]
        private List<Button3d> liftCallButtons;

        [Header("Buttons inside the lift")] [SerializeField]
        private List<Button3d> floorButtons;

        private LiftStates _liftStates;

        private List<Vector3> _liftCallButtonTransforms = new List<Vector3>();
        private List<Vector3> _floorButtonTransforms = new List<Vector3>();


        private Vector3 TargetPosition { get; set; }
        private int _floor;
        private PlatformController _platformController;

        private void Awake()
        {
            _liftStates = new LiftStates();
            TargetPosition = liftStopSpots[0].transform.position;
            _platformController = platform.GetComponent<PlatformController>();
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

        private void OnButtonClicked(string where, int floor)
        {
            Debug.Log($"<Color=Yellow>Button clicked: {where}: {floor}</Color>");
            platform.Destination = liftStopSpots[floor - 1].localPosition;
            _liftStates.IsMoving = true;
            _floor = floor;
        }
    }
}