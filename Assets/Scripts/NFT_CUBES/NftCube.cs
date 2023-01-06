using UnityEngine;
using UnityEngine.UI;

namespace NFT_CUBES
{
    public class NftCube : MonoBehaviour
    {
        private Button _nftButton;

        private void Awake()
        {
            _nftButton = GameObject.Find("Nft_Button").GetComponent<Button>();
        }

        private void Start()
        {
            
            _nftButton.transform.parent.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            _nftButton.transform.parent.gameObject.SetActive(true);
            _nftButton.onClick.AddListener(() => Application.OpenURL("https://opensea.io/"));
        }

        private void OnTriggerExit(Collider other)
        {
            _nftButton.onClick.RemoveAllListeners();
            _nftButton.transform.parent.gameObject.SetActive(false);
        }
    }
}