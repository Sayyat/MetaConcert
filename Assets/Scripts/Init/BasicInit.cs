using Photon.Pun;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BasicInit : MonoBehaviourPun
{
    private void Start()
    {
        PhotonNetwork.Instantiate("Players/Sayat", Vector3.up, Quaternion.identity);
    }
}