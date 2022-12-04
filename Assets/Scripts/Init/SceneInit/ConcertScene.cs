using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Init.SceneInit
{
    public class ConcertScene : MonoBehaviourPun,IScene
    {
        public Player OwnerPlayer { get; set; }
        public List<GameObject> Lifts { get; set; } = new List<GameObject>();
        
        public void StartScene()
        {
            
            
            // var lift =   PhotonNetwork.Instantiate("Lift", Vector3.back, Quaternion.identity);
            // Lifts.Add(lift);
        }

        private void ConstructAgora()
        {
            
        }
    }
}