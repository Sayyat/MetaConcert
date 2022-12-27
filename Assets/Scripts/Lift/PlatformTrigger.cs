using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lift
{
    public class PlatformTrigger : MonoBehaviour
    {
        public List<Transform> PlayersTransform { get; set; }
        
        private void Start()
        {
            PlayersTransform = new List<Transform>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            PlayersTransform.Add(other.transform);
            // other.transform.parent = transform;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            PlayersTransform.Remove(other.transform);
            // other.transform.parent = null;
        }
    }
}