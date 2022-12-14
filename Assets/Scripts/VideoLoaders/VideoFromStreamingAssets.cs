using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

namespace VideoLoaders
{
    public class VideoFromStreamingAssets : MonoBehaviour
    {
        [SerializeField] private List<VideoPlayer> videoPlayers;
        [SerializeField] private List<string> urls;

        private string _prefix = "file://";

        private void Start()
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_STANDALONE_OSX
            _prefix = "file://";
#else
            _prefix = "";
#endif
            for (var i = 0; i < videoPlayers.Count; i++)
            {
                var path = Path.Combine(_prefix, Application.streamingAssetsPath, $"{urls[i]}.webm");
                videoPlayers[i].url = path;
            }
        }
    }
}