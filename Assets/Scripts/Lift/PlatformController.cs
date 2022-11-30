using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lift
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> childPlayers = new List<GameObject>();
        [SerializeField] private GameObject playersParent;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("<Color=Green>Collision enter</Color>");
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                childPlayers.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("<Color=Red>Collision exit</Color>");
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                childPlayers.Remove(other.gameObject);
            }
        }

        
        
        public void AddPlayerChild()
        {
            foreach (var childPlayer in childPlayers)
            {
                childPlayer.transform.parent = playersParent.transform;
            }
        }

        public void RemovePlayerChild()
        {
            foreach (var childPlayer in childPlayers)
            {
                childPlayer.transform.parent = null;
            }
        }
    }
}