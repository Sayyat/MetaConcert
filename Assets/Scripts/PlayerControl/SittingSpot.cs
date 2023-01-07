using UnityEngine;

namespace PlayerControl
{
    public class SittingSpot : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<AnimationControl>().CanSit(true);
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
