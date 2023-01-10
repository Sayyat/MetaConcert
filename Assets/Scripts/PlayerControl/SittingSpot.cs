using System;
using UnityEngine;

namespace PlayerControl
{
    public class SittingSpot : MonoBehaviour
    {
        [SerializeField] private Transform spot;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<AnimationControl>().CanSit(true, spot);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<AnimationControl>().CanSit(false);
            }
        }
    }
}