using System;
using UnityEngine;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}