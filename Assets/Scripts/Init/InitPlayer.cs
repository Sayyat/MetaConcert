using System;
using Photon.Pun;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Init;
using UnityEngine;

public class InitPlayer : MonoBehaviourPun
{
    [SerializeField] private Avatar avatarSchemeMan;
    [SerializeField] private Avatar avatarSchemeWoman;
    [SerializeField] private GameObject readyPlayerPrefab;
    private GameObject readyPlayer;

    private bool IsConstructed = false;
    public ConstructAvatar Avatar { get; set; }

    private void Start()
    {
        PhotonNetwork.Instantiate("PlayerTemplate", Vector3.up, Quaternion.identity);
        readyPlayer = Instantiate(readyPlayerPrefab);
    }

    private void Update()
    {
        if (Avatar == null || IsConstructed)
            return;

        Avatar.Construct(readyPlayer);
        Avatar.SetupAvatarOnAnimator(avatarSchemeMan);
        Destroy(readyPlayer);
        IsConstructed = true;
    }
}