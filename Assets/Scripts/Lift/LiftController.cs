using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lift
{
    public class LiftController : MonoBehaviour
    {
        [SerializeField] private GameObject platform;

        [Header("Spots were lift can stop")] [SerializeField]
        private List<Transform> liftStopSpots;

        [Header("Buttons to call lift")] [SerializeField]
        private List<Button3d> liftCallButtons;

        [Header("Buttons inside the lift")] [SerializeField]
        private List<Button3d> floorButtons;


        private Animation _platformAnimation;

        private LiftStates _liftStates;

        private List<Vector3> _liftCallButtonTransforms = new List<Vector3>();
        private List<Vector3> _floorButtonTransforms = new List<Vector3>();

        private void Awake()
        {
            _liftStates = GetComponent<LiftStates>();
            liftCallButtons.ForEach(button =>
            {   
                button.ButtonClicked += OnButtonClicked;
            });
            
            floorButtons.ForEach(button =>
            {   
                button.ButtonClicked += OnButtonClicked;
            });
        }

        
        // private void OnDisable()
        // {
        //     liftCallButtons.ForEach(button =>
        //     {
        //         button.ButtonClicked -= OnButtonClicked;
        //     });
        //     floorButtons.ForEach(button =>
        //     {   
        //         button.ButtonClicked -= OnButtonClicked;
        //     });
        // }
        private void OnButtonClicked(string where, int floor)
        {
            Debug.Log($"<Color=Yellow>Button clicked: {where}: {floor}</Color>");
            MoveLift(liftStopSpots[floor - 1].position);
        }

        private void MoveLift(Vector3 targetPosition)
        {
            Debug.Log("<Color=Red>Lift started moving</Color>");
            platform.transform.position =
                Vector3.MoveTowards(platform.transform.position, targetPosition, 0.5f * Time.deltaTime);
        }
    }
}