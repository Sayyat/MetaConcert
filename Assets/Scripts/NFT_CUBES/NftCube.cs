using UnityEngine;
using UnityEngine.UI;

namespace NFT_CUBES
{
    public class NftCube : MonoBehaviour
    {
        private Button nftButton;
        
        public void Init(Button newNftButton)
        {
            nftButton = newNftButton;
            nftButton.transform.parent.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            nftButton.transform.parent.gameObject.SetActive(true);
            nftButton.onClick.AddListener(() => Application.OpenURL("https://opensea.io/"));
        }

        private void OnTriggerExit(Collider other)
        {
            nftButton.onClick.RemoveAllListeners();
            nftButton.transform.parent.gameObject.SetActive(false);
        }
    }
}