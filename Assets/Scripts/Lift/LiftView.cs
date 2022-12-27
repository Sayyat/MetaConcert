
using Photon.Pun;
using UnityEngine;

namespace Lift
{
    public class LiftView : MonoBehaviour, IPunInstantiateMagicCallback
    {
 
        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            var parent = GameObject.Find("Lifts");
            var pos = transform.position;
            transform.parent = parent.transform;
            transform.localPosition = pos;
            gameObject.SetActive(true);

        }
    }
}