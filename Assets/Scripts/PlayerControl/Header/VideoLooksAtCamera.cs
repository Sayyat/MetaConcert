using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace PlayerControl.Header
{
    public class VideoLooksAtCamera : MonoBehaviour
    {
        private Camera _cam;

        private void Start()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            transform.LookAt(_cam.transform);
            transform.Rotate(Vector3.right * 180);
        }
    }
}