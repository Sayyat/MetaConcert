using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lift
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private GameObject playersParent;


        private Vector3 _destination;

        private Vector3 _step = new Vector3(0f,0f,0.1f);

        private bool IsMoving { get; set; }

        public Vector3 Destination
        {
            get => _destination;
            set
            {
                _destination = value;

                _step = new Vector3(0f,0f,(Destination.z - transform.localPosition.z) / 10f);
                IsMoving = true;
            }
        }


        private void Update()
        {
            if(!IsMoving) return;

            if (Vector3.Distance(transform.localPosition, _destination) < 0.01)
            {
                IsMoving = false;
            }
            
            // transform.position =
            //     Vector3.MoveTowards(transform.position, _destination, 2f * Time.deltaTime);

            transform.localPosition += _step * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("<Color=Green>Collision enter</Color>");
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.transform.parent = playersParent.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("<Color=Red>Collision exit</Color>");
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.transform.parent = null;
            }
        }
    }
}