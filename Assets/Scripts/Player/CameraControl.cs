using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CameraControl : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform cameraTarget;
    private new CinemachineVirtualCamera camera;

    private void Start()
    {
        if (photonView.IsMine)
        {
            camera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            camera.Follow = cameraTarget;
        }
    }

    public override void OnJoinedRoom()
    {
        camera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        camera.Follow = cameraTarget;
    }
}