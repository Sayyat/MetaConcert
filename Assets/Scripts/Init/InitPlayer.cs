using System.IO;
using Photon.Pun;
using StarterAssets;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class InitPlayer : MonoBehaviourPun
{


    [SerializeField] private GameObject playerPrefab;
  

    private void Start()
    {
        
        var player = PhotonNetwork.Instantiate("PlayerTemplate", Vector3.up, Quaternion.identity );
        
       
        
    }
}