using UnityEngine;
using UnityEngine.SceneManagement;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Transform backgroundConcert;
        [SerializeField] private AudioSource backgroundAudioSourceConcert;
        private void Awake()
        {
            DontDestroyOnLoad(this);
            SceneManager.activeSceneChanged += (arg0, scene) =>
            {
                if (scene.name == "Concert")
                {
                    transform.position =backgroundConcert.position;
                    audioSource.spatialBlend = 1;
                    audioSource.maxDistance = backgroundAudioSourceConcert.maxDistance;
                }
                else
                {
                    audioSource.spatialBlend = 0;
                }
            };
        }
    }
}