using System;
using Photon.Pun;
using UnityEngine;

namespace Lift
{
    public class LiftView : MonoBehaviourPun, IPunInstantiateMagicCallback
    {
        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            var x = Convert.ToSingle(info.photonView.InstantiationData[0]);
            var y = Convert.ToSingle(info.photonView.InstantiationData[1]);
            var z = Convert.ToSingle(info.photonView.InstantiationData[2]);
            var parent = info.photonView.InstantiationData[3] as GameObject;

            transform.parent = parent.transform;
            transform.localPosition = new Vector3(x, y, z);
        }
    }
}