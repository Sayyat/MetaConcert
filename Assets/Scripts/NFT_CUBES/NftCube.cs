using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace NFT_CUBES
{
    public class NftCube : MonoBehaviourPun
    {
        private Button _nftButton;

        public void Init(Button newNftButton)
        {
            if (!photonView.IsMine) return;
            _nftButton = newNftButton;
            _nftButton.transform.parent.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine) return;
            _nftButton.transform.parent.gameObject.SetActive(true);
            _nftButton.onClick.AddListener(() => Application.OpenURL("https://opensea.io/"));
        }

        private void OnTriggerExit(Collider other)
        {
            if (!photonView.IsMine) return;
            _nftButton.onClick.RemoveAllListeners();
            _nftButton.transform.parent.gameObject.SetActive(false);
        }
    }
}