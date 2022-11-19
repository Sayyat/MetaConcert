using System;
using System.Collections.Generic;
using Photon.Pun;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Init;
using ReadyPlayerMe;
using UnityEngine;

public class InitPlayer : MonoBehaviourPun
{
    public Avatar avatarSchemeMan;
    public Avatar avatarSchemeWoman;
    public GameObject defaultAvatar;

    private void Start()
    {
        PhotonNetwork.Instantiate("PlayerTemplate", Vector3.up, Quaternion.identity);
    }
}