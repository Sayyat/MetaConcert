﻿
using System.Collections.Generic;
using UnityEngine;

namespace Lift
{
    public class PlatformMove : MonoBehaviour
    {
        public List<Transform> players;
        public List<Vector3> initialPositions;

        private Vector3 _destination;

        private bool IsMoving { get; set; }

        public Vector3 Destination
        {
            get => _destination;
            set
            {
                _destination = value;
                IsMoving = true;
            }
        }


        private void LateUpdate()
        {
            if (!IsMoving)
            {
                enabled = false;
                
            }

            if (Vector3.Distance(transform.position, _destination) < 0.001)
            {
                IsMoving = false;
            }

            transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime);

            for (var i = 0; i < players.Count; i++)
            {
                var pTransform = players[i];
                pTransform.localPosition = initialPositions[i];
            }
        }
    }
}