using UnityEngine;

namespace Effects
{
    public class DisableObject : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_ANDROID || UNITY_WEBGL
            gameObject.SetActive(false);
#endif
        }
    }
}