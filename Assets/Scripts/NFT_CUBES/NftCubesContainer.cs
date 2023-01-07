using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NFT_CUBES
{
    public class NftCubesContainer : MonoBehaviour
    {
        [SerializeField]private List<NftCube> cubes;

        public void SetNftButton(Button nftButton)
        {
            foreach (var cube in cubes)
            {
                cube.Init(nftButton);
            }
        }

    }
}
