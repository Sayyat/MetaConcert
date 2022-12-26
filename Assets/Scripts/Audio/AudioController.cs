using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Transform backgroundConcert; 
        private void Awake()
        {
            DontDestroyOnLoad(this);
            SceneManager.activeSceneChanged += (arg0, scene) =>
            {
                if (scene.name == "Concert")
                {
                    transform.position =backgroundConcert.position;
                    _audioSource.spatialBlend = 1;
                }
            };
        }
    }
}