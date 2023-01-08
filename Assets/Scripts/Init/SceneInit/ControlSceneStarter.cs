using System;
using Agora;
using NFT_CUBES;
using Photon.Pun;
using Photon.Realtime;
using PlayerControl;
using Quest;
using StarterAssets;
using UI;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Init.SceneInit
{
    public class ControlSceneStarter : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private GameObject playerFollowCamera;
        
        [Header("Spawn point settings")] [SerializeField]
        private Vector3 minSpawnPoint;

        [SerializeField] private Vector3 maxSpawnPoint;
        

        private PhotonView _photonView;

        private GameObject _photonPlayer;

        private void Awake()
        {
            Instantiate(mainCamera);
            Instantiate(playerFollowCamera);
        }

        private void Start()
        {
            // calculate random position
            var x = Random.Range(Math.Min(minSpawnPoint.x, maxSpawnPoint.x),
                Math.Max(minSpawnPoint.x, maxSpawnPoint.x));
            var y = Random.Range(Math.Min(minSpawnPoint.y, maxSpawnPoint.y),
                Math.Max(minSpawnPoint.y, maxSpawnPoint.y));
            var z = Random.Range(Math.Min(minSpawnPoint.z, maxSpawnPoint.z),
                Math.Max(minSpawnPoint.z, maxSpawnPoint.z));

            _photonPlayer = PhotonNetwork.Instantiate("PlayerTemplate", new Vector3(x, y, z), Quaternion.identity);
            _photonView = _photonPlayer.GetComponent<PhotonView>();
            

            // add my photon id to current room props
            var myId = _photonView.Owner.ActorNumber.ToString();
            var customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            customProperties.Add(myId, null);
            PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);
        }
    }
}