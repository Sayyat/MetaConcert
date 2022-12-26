using UnityEngine;

namespace NFT_CUBES
{
    public class LinkOpener : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Application.OpenURL("https://opensea.io/");
        }
    }
}
