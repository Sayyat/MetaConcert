using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class VideoLooksAtCamera : MonoBehaviour
{
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(_cam.transform);
        transform.Rotate(Vector3.right * 180);
    }
}