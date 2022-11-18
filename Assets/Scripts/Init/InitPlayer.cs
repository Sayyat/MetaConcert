using Photon.Pun;
using StarterAssets;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class InitPlayer : MonoBehaviourPun
{
    private GameObject playerPrefab;
    private GameObject player;

    
    private PhotonAnimatorView _photonAnimatorView;
    private PhotonTransformView _photonTransformView;
    private PhotonView _photonView;
    
    private ThirdPersonController _controller;
    
    private CameraControl _cameraControl;
    private StarterAssetsInputs _starterAssetsInputs;
    private  BasicRigidBodyPush _basicRigidBodyPush;
    private void Start()
    {
        
        player = new GameObject("PlayerPrefab");

        
        Instantiate(player, Vector3.up, Quaternion.identity);
        
        _controller = player.AddComponent<ThirdPersonController>();
        var targetGO = new GameObject("CameraTarget")
        {
            transform =
            {
                parent = player.transform,
                position = new Vector3(0f, 1.66f, -0.76f)
            }
        };
        
        _controller.CinemachineCameraTarget = targetGO;

        
        
        
        if (!Directory.Exists("Assets/Resources/Dynamic"))
            AssetDatabase.CreateFolder("Assets/Resources", "Dynamic");
        string localPath = "Assets/Resources/Dynamic/" + player.name + ".prefab";
        playerPrefab = PrefabUtility.SaveAsPrefabAsset(player, localPath, out var success);
        Debug.Log($"Prefab created: {success}" );
        
  
        // PhotonNetwork.Instantiate($"Players/{playerPrefab.name}", Vector3.up, Quaternion.identity );
    }
    
}
