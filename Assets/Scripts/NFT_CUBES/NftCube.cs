using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace NFT_CUBES
{
    public class NftCube : MonoBehaviour
    {
        private Button _nftButton;
        private PhotonView _photonView;
        public void Init(Button newNftButton, PhotonView photonView)
        {
            _photonView = photonView;
            _nftButton = newNftButton;
            _nftButton.transform.parent.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_photonView.IsMine) return;
            _nftButton.transform.parent.gameObject.SetActive(true);
            _nftButton.onClick.AddListener(() => Application.OpenURL("https://opensea.io/"));
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_photonView.IsMine) return;
            _nftButton.onClick.RemoveAllListeners();
            _nftButton.transform.parent.gameObject.SetActive(false);
        }
    }
}