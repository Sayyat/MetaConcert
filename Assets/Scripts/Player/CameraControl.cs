using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    private CinemachineVirtualCamera camera;

    private void Start()
    {
        camera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        camera.Follow = cameraTarget;
    }
}