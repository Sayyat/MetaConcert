using Photon.Pun;
using UnityEngine;

namespace Quest
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int value;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger");
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

            var photonView = other.gameObject.GetComponent<PhotonView>();
            if(!photonView.IsMine) return;
            
            var collector = other.gameObject.GetComponent<Collector>();
            collector.AddCoin(value);

            gameObject.SetActive(false);
        }
    }
}