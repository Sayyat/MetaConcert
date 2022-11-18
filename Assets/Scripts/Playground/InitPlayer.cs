using Cinemachine;
using Photon.Pun;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class InitPlayer : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject playerPrefab;
    private void Start()
    {
        PhotonNetwork.Instantiate($"Players/{playerPrefab.name}", Vector3.up, Quaternion.identity );
    }
    
}
