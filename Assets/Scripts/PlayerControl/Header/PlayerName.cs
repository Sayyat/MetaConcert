using Photon.Pun;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace PlayerControl.Header
{
    public class PlayerName : MonoBehaviourPun
    {
        private Camera _cam;
        private TextMeshPro _text;
        private void Start()
        {
            _cam = Camera.main;
            _text = GetComponent<TextMeshPro>();
            _text.text = photonView.Owner.NickName;
        }

        private void Update()
        {
            transform.LookAt(_cam.transform);
            transform.Rotate(Vector3.up * 180);
        }
    }
}
