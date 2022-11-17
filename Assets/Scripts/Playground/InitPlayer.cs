using Cinemachine;
using Photon.Pun;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class InitPlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private CinemachineVirtualCamera followCamera;
    private void Start()
    {
        PhotonNetwork.Instantiate("Sphere", Vector3.up, Quaternion.identity );
        FollowCamera();
    }


  
     void FollowCamera()
        {
            Debug.Log("Atempt to follow");
    
            // followerCamera = GetComponent<CinemachineVirtualCamera>();
            var playerCamera = GameObject.FindWithTag("CinemachineTarget");
    
            // if(playerCamera == null)
            // playerCamera = playerPrefab.transform.GetChild(0).gameObject;
            if (playerCamera != null)
            {
                followCamera.Follow = playerCamera.transform;
                Debug.Log("Started Following after");
            }
        }
}
