using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        
        [SerializeField] private AudioSource _audioSource;  
        private void Awake()
        {
            DontDestroyOnLoad(this);
            SceneManager.activeSceneChanged += (arg0, scene) =>
            {
                if (scene.name == "Concert")
                {
                    transform.position = Vector3.zero;
                    _audioSource.spatialBlend = 0;
                }
            };
        }
    }
}