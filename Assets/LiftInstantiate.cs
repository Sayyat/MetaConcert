using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LiftInstantiate : MonoBehaviour
{
    private void Start()
    {
        PhotonNetwork.Instantiate("Lift", Vector3.zero, Quaternion.Euler(-90f, 0f, 0f), 0);
    }
}
