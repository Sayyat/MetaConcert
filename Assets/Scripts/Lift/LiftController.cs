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
        private List<GameObject> liftCallButtons;
        
        [Header("Buttons inside the lift")] [SerializeField]
        private List<GameObject> floorButtons;

        private Animation _platformAnimation;

        private LiftStates _liftStates;

        private List<Transform> _liftCallButtonTransforms;
        private List<Transform> _floorButtonTransforms;

        private void Awake()
        {
            _liftStates = GetComponent<LiftStates>();
            liftCallButtons.ForEach(button =>
            {
                _liftCallButtonTransforms.Add(button.transform);
            });
            
            floorButtons.ForEach(button =>
            {
                _floorButtonTransforms.Add(button.transform);
            });
        }

        private void Start()
        {
            
        }


      
        
        void Update() {  
            if (Input.GetMouseButtonDown(0)) {  
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
                RaycastHit hit;  
                if (Physics.Raycast(ray, out hit)) {  
                    //Select stage    
                    if (_liftCallButtonTransforms.Contains(hit.transform)) {
                        Debug.Log("<Color=Red>liftCallButton pressed</Color>");
                    }  
                    if (_floorButtonTransforms.Contains(hit.transform)) {
                        Debug.Log("<Color=Red>floorButtons pressed</Color>");
                    }  

                    
                }  
            }  
        }  

        private void MoveLift(Vector3 targetPosition)
        {
            Debug.Log("<Color=Red>Lift started moving</Color>");
            platform.transform.position =
                Vector3.MoveTowards(platform.transform.position, targetPosition,0.02f * Time.deltaTime);
        }
    }
}