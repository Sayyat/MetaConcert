using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace NFT_CUBES
{
    public class NftCubesContainer : MonoBehaviour
    {
        [SerializeField]private List<NftCube> cubes;

        public void SetNftButton(Button nftButton, PhotonView photonView)
        {
            foreach (var cube in cubes)
            {
                cube.Init(nftButton, photonView);
            }
        }

    }
}
