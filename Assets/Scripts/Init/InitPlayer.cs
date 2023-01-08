using Photon.Pun;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
namespace Init
{
    public class InitPlayer : MonoBehaviourPun
    {
   
        private void Start()
        {
            PhotonNetwork.Instantiate("PlayerTemplate", Vector3.up, Quaternion.identity);
        }
    }
}