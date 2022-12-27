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

        private void Update()
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
        }
    }
}