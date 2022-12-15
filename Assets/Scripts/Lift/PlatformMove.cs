using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lift
{
    public class PlatformMove : MonoBehaviour
    {
        public List<Transform> players;

        private Vector3 _destination;

        private bool IsMoving { get; set; }

        public Vector3 Destination
        {
            get => _destination;
            set
            {
                foreach (var player in players)
                {
                    player.GetComponent<CharacterController>().enabled = false;
                }

                _destination = value;
                IsMoving = true;
            }
        }


        private void Update()
        {
            if (!IsMoving)
            {
                foreach (var player in players)
                {
                    player.GetComponent<CharacterController>().enabled = true;
                }

                enabled = false;
            }

            if (Vector3.Distance(transform.position, _destination) < 0.001)
            {
                IsMoving = false;
            }

            transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime);
        }
    }
}